using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Thinktecture.Relay.Server.DependencyInjection;
using Thinktecture.Relay.Server.Middleware;
using Thinktecture.Relay.Transport;

// ReSharper disable once CheckNamespace; (extension methods on IApplicationBuilder namespace)
namespace Microsoft.AspNetCore.Builder
{
	/// <summary>
	/// Extension methods for the <see cref="IApplicationBuilder"/>.
	/// </summary>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Adds RelayServer to the application's request pipeline.
		/// </summary>
		/// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
		/// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
		public static IApplicationBuilder UseRelayServer(this IApplicationBuilder builder) => builder.UseRelayServer<ClientRequest>();

		/// <summary>
		/// Adds RelayServer to the application's request pipeline.
		/// </summary>
		/// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
		/// <typeparam name="TRequest">The type of request.</typeparam>
		/// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
		public static IApplicationBuilder UseRelayServer<TRequest>(this IApplicationBuilder builder)
			where TRequest : IRelayClientRequest
		{
			builder.Map("/relay", app => app.UseMiddleware<RelayMiddleware<TRequest>>());
			builder.Map("/health", app =>
			{
				app.UseHealthChecks("/ready", new HealthCheckOptions() { Predicate = check => check.Tags.Contains("ready") });
				app.UseHealthChecks("/live", new HealthCheckOptions() { Predicate = _ => false });
			});

			foreach (var part in builder.ApplicationServices.GetServices<IApplicationBuilderPart>())
			{
				part.Use(builder);
			}

			return builder;
		}
	}
}
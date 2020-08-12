using System;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Thinktecture.Relay.Server.Services
{
	/// <summary>
	/// An implementation that creates a discovery document.
	/// </summary>
	public class DiscoveryDocumentBuilder
	{
		private readonly ILogger<DiscoveryDocumentBuilder> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscoveryDocumentBuilder"/>.
		/// </summary>
		/// <param name="logger">An <see cref="ILogger"/> to log to.</param>
		/// <param name="httpContextAccessor">An <see cref="IHttpContextAccessor"/> to retrieve the http context from.</param>
		/// <param name="serviceProvider">An <see cref="IServiceProvider"/> to retrieve services from.</param>
		public DiscoveryDocumentBuilder(ILogger<DiscoveryDocumentBuilder> logger, IHttpContextAccessor httpContextAccessor,
			IServiceProvider serviceProvider)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		/// <summary>
		/// Builds a discovery document.
		/// </summary>
		/// <returns>A new instance of the discovery document.</returns>
		public DiscoveryDocument BuildDiscoveryDocument()
		{
			var baseUrl = BuildBaseUrl();

			return new DiscoveryDocument()
			{
				ServerVersion = GetServerVersion(),
				AuthorizationServer = GetAuthorizationServer(),
				ConnectorEndpoint = baseUrl + "connector",
				RequestEndpoint = baseUrl + "body/request",
				ResponseEndpoint = baseUrl + "body/response",
				ConnectionTimeout = (int)TimeSpan.FromSeconds(30).TotalSeconds,
				ReconnectMinDelay = (int)TimeSpan.FromSeconds(30).TotalSeconds,
				ReconnectMaxDelay = (int)TimeSpan.FromMinutes(5).TotalSeconds,
			};
		}

		private string GetServerVersion()
		{
			return GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
				?? GetType().Assembly.GetCustomAttribute<AssemblyVersionAttribute>()?.Version;
		}

		private string GetAuthorizationServer()
		{
			var optionsMonitor = _serviceProvider.GetService<IOptionsMonitor<JwtBearerOptions>>();
			var options = optionsMonitor?.Get(Constants.DefaultAuthenticationScheme);

			return options?.Authority;
		}

		private string BuildBaseUrl()
		{
			var httpContext = _httpContextAccessor.HttpContext;

			var scheme = httpContext.Request.Scheme;
			var host = httpContext.Request.Host.ToUriComponent();
			var basePath = httpContext.Request.PathBase.Value?.TrimEnd('/');

			var baseUrl = $"{scheme}://{host}{basePath}";
			// ensure trailing slash
			if (!baseUrl.EndsWith("/"))
			{
				baseUrl += "/";
			}

			_logger?.LogDebug("Base url '{BaseUrl}' was build from: {Scheme}, {Host}, {BasePath}", baseUrl, scheme, host, basePath);

			return baseUrl;
		}
	}
}

using System.Threading;
using System.Threading.Tasks;
using Thinktecture.Relay.Transport;

namespace Thinktecture.Relay.Connector.RelayTargets
{
	/// <summary>
	/// A client request handler determining the <see cref="IRelayTarget{TRequest,TResponse}"/> handling the request.
	/// </summary>
	/// <typeparam name="TRequest">The type of request.</typeparam>
	/// <typeparam name="TResponse">The type of response.</typeparam>
	public interface IClientRequestHandler<in TRequest, TResponse>
		where TRequest : IClientRequest
		where TResponse : ITargetResponse
	{
		/// <summary>
		/// Called when a request was received.
		/// </summary>
		/// <param name="request">The client request.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation, which wraps the response.</returns>
		Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
	}
}
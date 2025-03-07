using System.Net.Http;
using Thinktecture.Relay.Connector.Targets;

namespace Thinktecture.Relay.Connector;

/// <summary>
/// Constants for the connector.
/// </summary>
public static class Constants
{
	/// <summary>
	/// The name of the client credentials client.
	/// </summary>
	public static readonly string RelayServerClientName = $"{typeof(Constants).Namespace}.ClientCredentials";

	/// <summary>
	/// The identity scopes.
	/// </summary>
	public const string RelayServerScopes = "connector";

	/// <summary>
	/// The id for the catch-all <see cref="IRelayTarget"/>.
	/// </summary>
	public const string RelayTargetCatchAllId = "** CATCH-ALL **";

	/// <summary>
	/// The name of the configuration key in a target definition for the id.
	/// </summary>
	public const string RelayConnectorOptionsTargetId = "Id";

	/// <summary>
	/// The name of the configuration key in a target definition for the type.
	/// </summary>
	public const string RelayConnectorOptionsTargetType = "Type";

	/// <summary>
	/// The name of the configuration key in a target definition for the timeout.
	/// </summary>
	public const string RelayConnectorOptionsTargetTimeout = "Timeout";

	/// <summary>
	/// Constants for HTTP headers.
	/// </summary>
	public static class HeaderNames
	{
		/// <summary>
		/// Contains the url to use for acknowledging a request by issuing as POST with an empty body to it.
		/// </summary>
		/// <remarks>This will only be present when manual acknowledgement is needed.</remarks>
		public const string AcknowledgeUrl = "RelayServer-AcknowledgeUrl";

		/// <summary>
		/// Contains the unique id of the request.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string RequestId = "RelayServer-RequestId";

		/// <summary>
		/// The unique id of the origin receiving the request.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string RequestOriginId = "RelayServer-RequestOriginId";

		/// <summary>
		/// Contains the machine name of the connector handling the request.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string ConnectorMachineName = "RelayServer-Connector-MachineName";

		/// <summary>
		/// Contains the version of the connector handling the request.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string ConnectorVersion = "RelayServer-Connector-Version";

		/// <summary>
		/// Contains the unique id of the origin to use for acknowledgement.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string AcknowledgeOriginId = "RelayServer-AcknowledgeOriginId";

		/// <summary>
		/// Contains the start timestamp of the target handling the request.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string TargetStart = "RelayServer-TargetStart";

		/// <summary>
		/// Contains the duration of the target handling the request.
		/// </summary>
		/// <remarks>This will only be present when tracing is enabled.</remarks>
		public const string TargetDuration = "RelayServer-TargetDuration";
	}

	/// <summary>
	/// Constants for named <see cref="HttpClient"/>.
	/// </summary>
	public static class HttpClientNames
	{
		/// <summary>
		/// The name of the <see cref="HttpClient"/> used for communicating with the server.
		/// </summary>
		public static readonly string RelayServer = $"{typeof(Constants).Namespace}.{nameof(RelayServer)}";

		/// <summary>
		/// The name of the default <see cref="HttpClient"/>.
		/// </summary>
		public static readonly string RelayWebTargetDefault =
			$"{typeof(Constants).Namespace}.{nameof(RelayWebTargetDefault)}";

		/// <summary>
		/// The name of the <see cref="HttpClient"/> following redirects.
		/// </summary>
		public static readonly string RelayWebTargetFollowRedirect =
			$"{typeof(Constants).Namespace}.{nameof(RelayWebTargetFollowRedirect)}";

		/// <summary>
		/// The name of the <see cref="HttpClient"/> with connection close header set.
		/// </summary>
		public static readonly string ConnectionClose =
			$"{typeof(Constants).Namespace}.{nameof(ConnectionClose)}";
	}
}

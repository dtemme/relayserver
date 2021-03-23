using System;
using System.Web.Http;
using Thinktecture.Relay.Server.Config;

namespace Thinktecture.Relay.Server.Helper
{
	public class ConfigurationDummy : IConfiguration
	{
		// RabbitMQ Settings
		public string RabbitMqConnectionString { get; set; }
		public string RabbitMqClusterHosts { get; set; }
		public bool RabbitMqAutomaticRecoveryEnabled { get; set; }
		public TimeSpan QueueExpiration { get; set; }
		public TimeSpan RequestExpiration { get; set; }

		// App Settings
		public TimeSpan OnPremiseConnectorCallbackTimeout { get; set; }
		public string TraceFileDirectory { get; set; }
		public int LinkPasswordLength { get; set; }
		public int DisconnectTimeout { get; set; }
		public int ConnectionTimeout { get; set; }
		public int KeepAliveInterval { get; set; }
		public bool UseInsecureHttp { get; set; }
		public ModuleBinding EnableManagementWeb { get; set; }
		public ModuleBinding EnableRelaying { get; set; }
		public ModuleBinding EnableOnPremiseConnections { get; set; }
		public string HostName { get; set; }
		public int Port { get; set; }
		public string ManagementWebLocation { get; set; }
		public TimeSpan TemporaryRequestStoragePeriod { get; set; }
		public string TemporaryRequestStoragePath { get; set; }
		public TimeSpan ActiveConnectionTimeout { get; set; }
		public string CustomCodeAssemblyPath { get; set; }
		public string SharedSecret { get; set; }
		public string OAuthCertificate { get; set; }
		public TimeSpan HstsHeaderMaxAge { get; set; }
		public bool HstsIncludeSubdomains { get; set; }
		public IncludeErrorDetailPolicy IncludeErrorDetailPolicy { get; set; }
		public int MaxFailedLoginAttempts { get; set; }
		public TimeSpan FailedLoginLockoutPeriod { get; set; }
		public bool SecureClientController { get; set; }
		public TimeSpan AccessTokenLifetime { get; set; }
		public bool LogSensitiveData { get; set; }
		public Boolean RequireLinkAvailability { get; set; }

		// Default settings for links
		public TimeSpan LinkTokenRefreshWindow { get; set; }
		public TimeSpan LinkReconnectMinWaitTime { get; set; }
		public TimeSpan LinkReconnectMaxWaitTime { get; set; }
		public TimeSpan? LinkAbsoluteConnectionLifetime { get; set; }
		public TimeSpan? LinkSlidingConnectionLifetime { get; set; }

		public ConfigurationDummy()
		{
			TemporaryRequestStoragePeriod = TimeSpan.FromMinutes(1);
		}
	}
}

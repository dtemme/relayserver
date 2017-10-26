using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Thinktecture.Relay.Server.Configuration;

namespace Thinktecture.Relay.Server.SignalR
{
	internal class InMemoryPostDataTemporaryStore : IPostDataTemporaryStore, IDisposable
	{
		private static readonly TimeSpan _cleanupInterval = TimeSpan.FromSeconds(1);
		private static readonly byte[] _emptyByteArray = new byte[0];

		private readonly ILogger _logger;
		private readonly TimeSpan _storagePeriod;

		private readonly ConcurrentDictionary<string, Entry> _data;
		private readonly CancellationTokenSource _cancellationTokenSource;

		public InMemoryPostDataTemporaryStore(ILogger logger, IConfiguration configuration)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));
			if (configuration.TemporaryRequestStoragePeriod <= TimeSpan.Zero)
				throw new ArgumentException($"{nameof(InMemoryPostDataTemporaryStore)}: Storage period must be positive. Provided value: {configuration.TemporaryRequestStoragePeriod}", nameof(configuration));

			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_storagePeriod = configuration.TemporaryRequestStoragePeriod;
			_data = new ConcurrentDictionary<string, Entry>();
			_cancellationTokenSource = new CancellationTokenSource();

			StartCleanUpTask(_cancellationTokenSource.Token);
		}

		private void StartCleanUpTask(CancellationToken token)
		{
			Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					while (!token.IsCancellationRequested)
					{
						CleanUp();
						await Task.Delay(_cleanupInterval, token).ConfigureAwait(false);
					}
				}
			}, token);
		}

		private void CleanUp()
		{
			foreach (var kvp in _data)
			{
				if (kvp.Value.IsTimedOut)
					_data.TryRemove(kvp.Key, out var value);
			}
		}

		public void Save(string requestId, byte[] data)
		{
			_logger.Debug($"{nameof(InMemoryPostDataTemporaryStore)}: Storing body for request id {{0}}", requestId);

			_data[requestId] = new Entry(data, _storagePeriod);
		}

		public byte[] Load(string requestId)
		{
			_logger.Debug($"{nameof(InMemoryPostDataTemporaryStore)}: Loading body for request id {{0}}", requestId);

			return _data.TryRemove(requestId, out var entry) ? entry.Data : _emptyByteArray;
		}

		~InMemoryPostDataTemporaryStore()
		{
			GC.SuppressFinalize(this);
		}

		public void Dispose()
		{
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
		}

		private class Entry
		{
			private readonly DateTime _timeoutDate;

			public byte[] Data { get; }

			public bool IsTimedOut => _timeoutDate < DateTime.UtcNow;

			public Entry(byte[] data, TimeSpan storagePeriod)
			{
				_timeoutDate = DateTime.UtcNow.Add(storagePeriod);
				Data = data;
			}
		}
	}
}
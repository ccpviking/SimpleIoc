using System;
using System.Threading;

namespace simple_ioc
{
	internal class IocScopeManager : IDisposable
	{
		private readonly IocEntry _iocEntry;

		public IocScopeManager(IocEntry entry, object factory)
		{
			_iocEntry = entry;
			Monitor.Enter(_iocEntry);
			_iocEntry.Push(factory);
		}

		// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Releases unmanaged resources and performs other cleanup operations before the
		// IocScopeManager is reclaimed by garbage collection.
		~IocScopeManager()
		{
			this.Dispose(false);
		}

		// Releases unmanaged and - optionally - managed resources
		private void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				_iocEntry.Pop();
				Monitor.Exit(_iocEntry);
			}
		}
	}
}
using System;
using System.Threading;

namespace DavidKiff.VendingMachine.UI.Extensions
{
    internal static class ReaderWriterLockExtensions
    {
        public static IDisposable ReadLock(this ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock.IsWriteLockHeld) return Disposable.Empty;

            readerWriterLock.EnterReadLock();
            return new Disposable(readerWriterLock.ExitReadLock);
        }

        public static IDisposable WriteLock(this ReaderWriterLockSlim readerWriterLock)
        {
            readerWriterLock.EnterWriteLock();
            return new Disposable(readerWriterLock.ExitWriteLock);
        }

        private sealed class Disposable : IDisposable
        {
            public static readonly IDisposable Empty = new Disposable();

            private Action dispose;

            private Disposable() { }

            public Disposable(Action dispose)
            {
                this.dispose = dispose;
                if (dispose == null)
                {
                    throw new ArgumentNullException("dispose");
                }
            }

            public void Dispose()
            {
                var action = Interlocked.Exchange(ref this.dispose, null);
                if (action == null)
                {
                    return;
                }
                action();
            }
        }
    }
}
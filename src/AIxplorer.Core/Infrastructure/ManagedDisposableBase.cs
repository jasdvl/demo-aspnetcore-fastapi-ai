using System.Diagnostics;

namespace AIxplorer.Core.Infrastructure;

// <summary>
/// Serves as a base class for objects responsible for releasing managed resources.
/// This class implements a basic disposable pattern suitable for classes that only deal with managed resources.
/// </summary>
/// <remarks>
/// If your class interacts with unmanaged resources, additional logic for handling these resources must be implemented. 
/// This includes overriding the finalizer and ensuring proper cleanup in the dispose method.
/// </remarks>
public abstract class ManagedDisposableBase : IDisposable
{
    /// <summary>
    /// Indicates whether the object has already been disposed.
    /// </summary>
    public bool IsDisposed => _disposeCount > 0;

    /// <summary>
    /// Tracks the disposal state of the object to prevent multiple disposals.
    /// </summary>
    protected int _disposeCount = 0;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting managed resources.
    /// </summary>
    public virtual void Dispose()
    {
        // Ensures DisposeManagedResources is called only once.
        if (Interlocked.Exchange(ref _disposeCount, 1) == 0)
        {
            DisposeManagedResources();
        }
    }

    /// <summary>
    /// Releases the managed resources used by the object. This method is called by the Dispose method.
    /// Derived classes should override this method to release their managed resources.
    /// </summary>
    protected virtual void DisposeManagedResources() { }

    /// <summary>
    /// Throws an ObjectDisposedException if the object has been disposed.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the object has been disposed.</exception>
    protected void ThrowIfDisposed()
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException(GetType().FullName);
        }
    }

    /// <summary>
    /// Asserts that the object has not been disposed. This method is only compiled in DEBUG builds.
    /// </summary>
    [Conditional("DEBUG")]
    protected void AssertNotDisposed()
    {
        Debug.Assert(!IsDisposed, "Object accessed after being disposed.");
    }
}

namespace ManagingState.Task3;

public sealed class ThreadSafeSingleton
{
    private static ThreadSafeSingleton? _instance;
    private static readonly object _instanceLock = new();

    public static ThreadSafeSingleton Instance
    {
        get
        {
            lock (_instanceLock)
            {
                return _instance ??= new ThreadSafeSingleton();
            }
        }
    }
    private ThreadSafeSingleton() { }
}
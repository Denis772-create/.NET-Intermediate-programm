namespace ManagingState.Task3;

public sealed class ThreadSafeSingletonWithoutLock
{
    public static ThreadSafeSingletonWithoutLock? Instance { get; }

    static ThreadSafeSingletonWithoutLock() { }
    private ThreadSafeSingletonWithoutLock() { }
}
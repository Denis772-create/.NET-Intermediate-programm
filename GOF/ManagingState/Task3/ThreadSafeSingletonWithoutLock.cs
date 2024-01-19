namespace ManagingState.Task3;

public sealed class ThreadSafeSingletonWithoutLock
{
    public static ThreadSafeSingletonWithoutLock Instance => new();

    static ThreadSafeSingletonWithoutLock() { }
    private ThreadSafeSingletonWithoutLock() { }
}
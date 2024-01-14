namespace ManagingState.Task3;

public sealed class NotThreadSafeSingleton
{
    private static NotThreadSafeSingleton? _instance;
    public static NotThreadSafeSingleton Instance => _instance ??= new NotThreadSafeSingleton();

    private NotThreadSafeSingleton() { }
}
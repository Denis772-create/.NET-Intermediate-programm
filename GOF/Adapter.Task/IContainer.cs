namespace Adapter.Task;

public interface IContainer<out T>
{
    IEnumerable<T> Items { get; }
    int Count { get; }
}
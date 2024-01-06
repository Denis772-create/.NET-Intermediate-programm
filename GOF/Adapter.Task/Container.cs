namespace Adapter.Task;

internal class Container<T> : IContainer<T>
{
    public IEnumerable<T> Items { get; }
    public int Count { get; }
}
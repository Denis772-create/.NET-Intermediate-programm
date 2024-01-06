namespace Adapter.Task;

internal class Elements<T> : IElements<T>
{
    private readonly List<T> _elements;

    public Elements(List<T> elements)
    {
        _elements = elements;
    }

    public IEnumerable<T> GetElements()
    {
        return _elements;
    }
}
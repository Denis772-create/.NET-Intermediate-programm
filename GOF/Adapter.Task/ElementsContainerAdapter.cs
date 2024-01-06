namespace Adapter.Task;

internal class ElementsContainerAdapter<T> : IContainer<T>
{
    private readonly IElements<T> _elements;

    public ElementsContainerAdapter(IElements<T> elements)
    {
        _elements = elements;
    }

    public IEnumerable<T> Items => _elements.GetElements();
    public int Count => _elements.GetElements().Count();
}
namespace Adapter.Task;

public interface IElements<out T>
{
    IEnumerable<T> GetElements();
}
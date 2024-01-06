namespace Adapter.Task;

internal class Program
{
    static void Main()
    {
        var stringElements = new Elements<string>(new List<string>
        {
            "Element 1", "Element 2", "Element 3"
        });
        var elementsAdapter = new ElementsContainerAdapter<string>(stringElements);

        var printer = new Printer();
        printer.Print(elementsAdapter);
    }
}
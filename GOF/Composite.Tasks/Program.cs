using Composite.Tasks.Task1;
using Composite.Tasks.Task2;

namespace Composite.Tasks;

internal class Program
{
    static void Main(string[] args)
    {
        var richTextForm = new Form("new form");
        richTextForm.AddComponent(new LabelText("Label value"));
        richTextForm.AddComponent(new InputText("input name", "input text value"));
            
        Console.WriteLine(richTextForm.ConvertToString());
    }
}
using Composite.Tasks.Task2;

namespace Composite.Tasks.Task1;

public class InputText : RichTextElement
{
    private readonly string _name;
    private readonly string _value;

    public InputText(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public override string ConvertToString()
    {
        return $"<inputText name='{_name}' value='{_value}'/>";
    }
}
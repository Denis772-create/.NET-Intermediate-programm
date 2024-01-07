using Composite.Tasks.Task2;

namespace Composite.Tasks.Task1;

public class LabelText : RichTextElement
{
    private readonly string _value;

    public LabelText(string value)
    {
        _value = value;
    }

    public override string ConvertToString()
    {
        return $"<label value='{_value}'/>";
    }
}
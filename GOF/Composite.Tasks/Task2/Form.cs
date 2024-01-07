namespace Composite.Tasks.Task2;

public class Form : RichTextElement
{
    private readonly List<RichTextElement> _childrenElements = new();
    private string _name;

    public Form(string name)
    {
        _name = name;
    }

    public override void AddComponent(RichTextElement richTextElement)
    {
        _childrenElements.Add(richTextElement);
    }

    public override string ConvertToString()
    {
        var childrenStrings = _childrenElements.Select(x => x.ConvertToString());
        return $"<form name='{_name}'>\n" +
               $"{string.Join("\n", childrenStrings)}\n" + 
               $"</form>";
    }
}

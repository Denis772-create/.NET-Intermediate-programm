namespace Composite.Tasks.Task2;

public abstract class RichTextElement
{
    public virtual void AddComponent(RichTextElement richTextElement)
    {
        throw new NotImplementedException();
    }

    public abstract string ConvertToString();
}
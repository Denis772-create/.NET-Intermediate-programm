namespace ManagingState.Task2.Models;

public class ValidateResult
{
    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; } = new();
}
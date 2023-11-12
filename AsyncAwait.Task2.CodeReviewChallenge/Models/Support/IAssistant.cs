using System.Threading.Tasks;

namespace Task2.CodeReviewChallenge.Models.Support;

public interface IAssistant
{
    Task<string> RequestAssistanceAsync(string requestInfo);
}

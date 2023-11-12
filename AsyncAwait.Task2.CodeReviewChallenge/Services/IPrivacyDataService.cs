using System.Threading.Tasks;

namespace Task2.CodeReviewChallenge.Services;

public interface IPrivacyDataService
{
    Task<string> GetPrivacyDataAsync();
}

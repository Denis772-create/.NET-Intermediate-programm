using ManagingState.Task2.Models;

namespace ManagingState.Task2.Validators;

public interface IFeedValidator<in T> where T : TradeFeed
{
    ValidateResult Validate(T feed);
}
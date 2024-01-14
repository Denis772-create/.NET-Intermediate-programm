using ManagingState.Task2.Models;

namespace ManagingState.Task2.Validators;

public abstract class BaseFeedValidator<T> : IFeedValidator<T> where T : TradeFeed
{
    public ValidateResult Validate(T feed)
    {
        var result = new ValidateResult();

        // Common validation rules
        if (feed.StagingId < 1 || feed.CounterpartyId < 1 || feed.PrincipalId < 1 || feed.SourceAccountId < 1)
        {
            result.ErrorMessages.Add(ErrorCode.InvalidStagingId); 
        }

        if (feed.CurrentPrice < 0 || decimal.Round(feed.CurrentPrice, 2) != feed.CurrentPrice)
        {
            result.ErrorMessages.Add("Invalid CurrentPrice"); 
        }

        CustomValidate(feed, result);

        result.IsValid = !result.ErrorMessages.Any();
        return result;
    }

    protected abstract void CustomValidate(T feed, ValidateResult result);
}

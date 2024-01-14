using ManagingState.Task2.Models;

namespace ManagingState.Task2.Validators;

public class EmFeedValidator : BaseFeedValidator<EmFeed>
{
    protected override void CustomValidate(EmFeed feed, ValidateResult result)
    {
        if (feed.Sedol is <= 0 or >= 100)
        {
            result.ErrorMessages.Add("Invalid Sedol"); 
        }

        if (feed.AssetValue <= 0 || feed.AssetValue >= feed.Sedol)
        {
            result.ErrorMessages.Add("AssetValue must be greater than 0 and less than Sedol"); 
        }
    }
}

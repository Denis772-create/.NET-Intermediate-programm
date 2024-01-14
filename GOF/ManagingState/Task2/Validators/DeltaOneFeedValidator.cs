using System.Text.RegularExpressions;
using ManagingState.Task2.Models;

namespace ManagingState.Task2.Validators;

public class DeltaOneFeedValidator : BaseFeedValidator<DeltaOneFeed>
{
    protected override void CustomValidate(DeltaOneFeed feed, ValidateResult result)
    {
        if (!Regex.IsMatch(feed.Isin, @"^[A-Z]{2}\d{10}$"))
        {
            result.ErrorMessages.Add("Invalid ISIN");
        }

        if (feed.MaturityDate <= feed.ValuationDate)
        {
            result.ErrorMessages.Add("MaturityDate should be greater than ValuationDate");
        }
    }
}
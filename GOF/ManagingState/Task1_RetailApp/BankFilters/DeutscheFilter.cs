using ManagingState.Task1.Entities;

namespace ManagingState.Task1.BankFilters;

public class DeutscheFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(trade => trade is { Type: "Option", SubType: "NewOption", Amount: > 90 and < 120 });
    }
}
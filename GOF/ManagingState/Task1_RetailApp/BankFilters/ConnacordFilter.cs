using ManagingState.Task1.Entities;

namespace ManagingState.Task1.BankFilters;

public class ConnacordFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(x => x.Amount is > 10 and < 20 && x.Type == "Future");
    }
}
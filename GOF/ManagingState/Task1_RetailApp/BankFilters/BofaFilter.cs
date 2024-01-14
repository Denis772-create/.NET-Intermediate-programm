using ManagingState.Task1.Entities;

namespace ManagingState.Task1.BankFilters;

internal class BofaFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(x => x.Amount > 70);
    }
}
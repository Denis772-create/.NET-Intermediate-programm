using ManagingState.Task1.Entities;

namespace ManagingState.Task1.BankFilters;

public interface IFilter
{
    IEnumerable<Trade> Match(IEnumerable<Trade> trades);
}
using ManagingState.Task1.Entities;

namespace ManagingState.Task1.BankFilters;

public class BarclaysFilter : IFilter
{
    private readonly Country _country;

    public BarclaysFilter(Country country)
    {
        _country = country;
    }

    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return _country switch
        {
            Country.USA => trades.Where(trade => trade is { Type: "Option", SubType: "NyOption", Amount: > 50 }),
            Country.England => trades.Where(trade => trade is { Type: "Future", Amount: > 100 }),
            _ => Enumerable.Empty<Trade>()
        };
    }
}
using ManagingState.Task1.BankFilters;
using ManagingState.Task1.Entities;

namespace ManagingState.Task1;

public class TradeFilter
{
    public IEnumerable<Trade> FilterForBank(IEnumerable<Trade> trades, Bank bank, Country country)
    {
        var filter = CreateFilter(bank, country);
        return filter.Match(trades);
    }

    public static IFilter CreateFilter(Bank bank, Country country)
    {
        return bank switch
        {
            Bank.Bofa => new BofaFilter(),
            Bank.Connacord => new ConnacordFilter(),
            Bank.Barclays => new BarclaysFilter(country),
            Bank.Deutsche => new DeutscheFilter(),
            _ => throw new NotImplementedException("Unsupported bank")
        };
    }
}
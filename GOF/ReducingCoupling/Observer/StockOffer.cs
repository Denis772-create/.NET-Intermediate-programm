namespace ReducingCoupling.Observer;

public class StockOffer
{
    public string StockName { get; }
    public int NumberOfShares { get; }
    public IPlayer Player { get; }

    public StockOffer(string stockName, int numberOfShares, IPlayer player)
    {
        StockName = stockName;
        NumberOfShares = numberOfShares;
        Player = player;
    }
}

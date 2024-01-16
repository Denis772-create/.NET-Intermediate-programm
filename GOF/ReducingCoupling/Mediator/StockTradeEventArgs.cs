namespace ReducingCoupling.Mediator;

public class StockTradeEventArgs : EventArgs
{
    public IPlayer Player { get; private set; }
    public string StockName { get; private set; }
    public int NumberOfShares { get; private set; }

    public StockTradeEventArgs(IPlayer player, string stockName, int numberOfShares)
    {
        Player = player;
        StockName = stockName;
        NumberOfShares = numberOfShares;
    }
}
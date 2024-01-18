namespace ReducingCoupling.Observer;

public class Blossomers : IPlayer
{
    private readonly StockExchange _mediator;

    public int SoldShares { get; private set; }
    public int BoughtShares { get; private set; }

    public Blossomers(StockExchange mediator)
    {
        _mediator = mediator;
    }

    public bool SellOffer(string stockName, int numberOfShares)
    {
        return _mediator.MakeOffer(new StockOffer(stockName, numberOfShares, this), true);
    }

    public bool BuyOffer(string stockName, int numberOfShares)
    {
        return _mediator.MakeOffer(new StockOffer(stockName, numberOfShares, this), false);
    }

    public void HandleNotification(string stockName, int numberOfShares, bool isSell)
    {
        if (isSell) SoldShares += numberOfShares;
        else BoughtShares += numberOfShares;
    }
}
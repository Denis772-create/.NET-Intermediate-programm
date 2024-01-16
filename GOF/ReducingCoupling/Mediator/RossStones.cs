namespace ReducingCoupling.Mediator;

public class RossStones : Player
{
    private readonly IStockMarket _mediator;


    public RossStones(IStockMarket mediator)
    {
        _mediator = mediator;

        mediator.SellOfferMatched += OnSellOfferMatched;
        mediator.BuyOfferMatched += OnBuyOfferMatched;
    }

    public override bool SellOffer(string stockName, int numberOfShares)
    {
        return _mediator.PlaceSellOffer(stockName, numberOfShares, this);
    }

    public override bool BuyOffer(string stockName, int numberOfShares)
    {
        return _mediator.PlaceBuyOffer(stockName, numberOfShares, this);
    }

    private void OnSellOfferMatched(object sender, StockTradeEventArgs e)
    {
        if (e.Player.Id == Id)
        {
            SoldShares += e.NumberOfShares;
        }
    }

    private void OnBuyOfferMatched(object sender, StockTradeEventArgs e)
    {
        if (e.Player.Id == Id)
        {
            BoughtShares += e.NumberOfShares;
        }
    }
}
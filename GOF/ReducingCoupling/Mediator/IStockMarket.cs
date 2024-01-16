namespace ReducingCoupling.Mediator;

public interface IStockMarket
{
    event EventHandler<StockTradeEventArgs> SellOfferMatched;
    event EventHandler<StockTradeEventArgs> BuyOfferMatched;

    bool PlaceSellOffer(string stockName, int numberOfShares, IPlayer trader);
    bool PlaceBuyOffer(string stockName, int numberOfShares, IPlayer trader);
}
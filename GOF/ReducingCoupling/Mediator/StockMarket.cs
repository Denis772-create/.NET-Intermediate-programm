namespace ReducingCoupling.Mediator;

public class StockMarket : IStockMarket
{
    public event EventHandler<StockTradeEventArgs> SellOfferMatched;
    public event EventHandler<StockTradeEventArgs> BuyOfferMatched;

    private readonly Dictionary<string, Queue<(IPlayer, int)>> _sellOffers = new();
    private readonly Dictionary<string, Queue<(IPlayer, int)>> _buyOffers = new();

    public bool PlaceSellOffer(string stockName, int numberOfShares, IPlayer buyer)
    {
        EnsureQueueExists(stockName, _sellOffers);

        if (_sellOffers.TryGetValue(stockName, out var sells) && sells.Count > 0)
        {
            var (seller, sharesToSell) = sells.Peek();
            if (seller.Id != buyer.Id && sharesToSell == numberOfShares)
            {
                sells.Dequeue();
                SellOfferMatched.Invoke(this, new StockTradeEventArgs(seller, stockName, numberOfShares));
                BuyOfferMatched.Invoke(this, new StockTradeEventArgs(buyer, stockName, numberOfShares));
                return true;
            }
        }

        _sellOffers[stockName].Enqueue((buyer, numberOfShares));
        return false;
    }

    public bool PlaceBuyOffer(string stockName, int numberOfShares, IPlayer buyer)
    {
        EnsureQueueExists(stockName, _buyOffers);

        if (_sellOffers.TryGetValue(stockName, out var sells) && sells.Count > 0)
        {
            var (seller, sharesToSell) = sells.Peek();
            if (seller.Id != buyer.Id && sharesToSell == numberOfShares)
            {
                sells.Dequeue();
                BuyOfferMatched.Invoke(this, new StockTradeEventArgs(buyer, stockName, numberOfShares));
                SellOfferMatched.Invoke(this, new StockTradeEventArgs(seller, stockName, numberOfShares));
                return true;
            }
        }

        _buyOffers[stockName].Enqueue((buyer, numberOfShares));
        return false;
    }

    private void EnsureQueueExists(string stockName, Dictionary<string, Queue<(IPlayer, int)>> offers)
    {
        if (!offers.ContainsKey(stockName))
        {
            offers[stockName] = new Queue<(IPlayer, int)>();
        }
    }
}
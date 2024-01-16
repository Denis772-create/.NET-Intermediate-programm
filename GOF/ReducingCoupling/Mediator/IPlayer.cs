namespace ReducingCoupling.Mediator;

public interface IPlayer
{
    Guid Id { get; }
    int SoldShares { get; set; }
    int BoughtShares { get; set; }

    bool SellOffer(string stockName, int numberOfShares);
    bool BuyOffer(string stockName, int numberOfShares);
}
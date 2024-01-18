namespace ReducingCoupling.Observer;

public interface IPlayer
{
    int SoldShares { get; }
    int BoughtShares { get; }
    bool SellOffer(string stockName, int numberOfShares);
    bool BuyOffer(string stockName, int numberOfShares);
    void HandleNotification(string stockName, int numberOfShares, bool isSell);
}
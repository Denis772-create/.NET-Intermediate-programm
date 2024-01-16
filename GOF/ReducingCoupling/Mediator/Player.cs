namespace ReducingCoupling.Mediator;

public abstract class Player : IPlayer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int SoldShares { get; set; }
    public int BoughtShares { get; set; }

    public abstract bool SellOffer(string stockName, int numberOfShares);
    public abstract bool BuyOffer(string stockName, int numberOfShares);
}
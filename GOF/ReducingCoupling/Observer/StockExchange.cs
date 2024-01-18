namespace ReducingCoupling.Observer;

public class StockExchange
{
    private readonly List<StockOffer> _buyOffers = new();
    private readonly List<StockOffer> _sellOffers = new();

    private void NotifyPlayers(StockOffer matchedOffer, StockOffer newOffer, bool isSelling)
    {
        if (isSelling)
        {
            matchedOffer.Player.HandleNotification(matchedOffer.StockName, matchedOffer.NumberOfShares, false); 
            newOffer.Player.HandleNotification(newOffer.StockName, newOffer.NumberOfShares, true); 
        }
        else
        {
            matchedOffer.Player.HandleNotification(matchedOffer.StockName, matchedOffer.NumberOfShares, true); 
            newOffer.Player.HandleNotification(newOffer.StockName, newOffer.NumberOfShares, false); 
        }
    }

    public bool MakeOffer(StockOffer offer, bool isSelling)
    {
        var oppositeOffers = isSelling ? _buyOffers : _sellOffers;

        foreach (var oppositeOffer in oppositeOffers)
        {
            if (oppositeOffer.StockName == offer.StockName && 
                oppositeOffer.NumberOfShares == offer.NumberOfShares && 
                oppositeOffer.Player != offer.Player)
            {
                oppositeOffers.Remove(oppositeOffer);
                NotifyPlayers(oppositeOffer, offer, isSelling);
                return true;
            }
        }

        if (isSelling) _sellOffers.Add(offer);
        else _buyOffers.Add(offer);

        return false;
    }
}
namespace AlteringBehavior.Task4;

public interface ITripRepository
{
    TripDetails LoadTrip(string touristName);
}
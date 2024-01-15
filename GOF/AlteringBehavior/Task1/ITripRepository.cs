namespace AlteringBehavior.Task1;

public interface ITripRepository
{
    TripDetails LoadTrip(string touristName);
}
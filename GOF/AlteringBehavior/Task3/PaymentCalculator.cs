namespace AlteringBehavior.Task3;

public class PaymentCalculator : ICalculator
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;

    public PaymentCalculator(
        ICurrencyService currencyService,
        ITripRepository tripRepository)
    {
        _currencyService = currencyService;
        _tripRepository = tripRepository;
    }

    public decimal CalculatePayment(string touristName)
    {
        // Load trip details
        TripDetails tripDetails = _tripRepository.LoadTrip(touristName);
        if (tripDetails == null)
        {
            throw new ArgumentException("Trip details not found for the given tourist name.");
        }

        // Current currency rate
        decimal rate = _currencyService.LoadCurrencyRate();
        if (rate <= 0)
        {
            throw new InvalidOperationException("Invalid currency rate.");
        }

        return Constants.A * rate * tripDetails.FlyCost +
               Constants.B * rate * tripDetails.AccomodationCost +
               Constants.C * rate * tripDetails.ExcursionCost;
    }
}
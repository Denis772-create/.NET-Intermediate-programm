using ManagingState.Task2.Models;

namespace ManagingState.Task2.Matchers;

public class DeltaOneFeedMatcher : IFeedMatcher<DeltaOneFeed>
{
    public bool Match(DeltaOneFeed current, DeltaOneFeed other)
    {
        return current.CounterpartyId == other.CounterpartyId && current.PrincipalId == other.PrincipalId;
    }
}
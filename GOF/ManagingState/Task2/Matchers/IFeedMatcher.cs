using ManagingState.Task2.Models;

namespace ManagingState.Task2.Matchers;

public interface IFeedMatcher<in T> where T : TradeFeed
{
    bool Match(T current, T other);
}
using ManagingState.Task2.Matchers;
using ManagingState.Task2.Models;
using ManagingState.Task2.Validators;

namespace ManagingState.Task2;

public class FeedFactory
{
    public static IFeedValidator<T> CreateValidator<T>() where T : TradeFeed
    {
        if (typeof(T) == typeof(DeltaOneFeed))
        {
            return (IFeedValidator<T>)new DeltaOneFeedValidator();
        }

        if (typeof(T) == typeof(EmFeed))
        {
            return (IFeedValidator<T>)new EmFeedValidator();
        }
        throw new ArgumentException("Unsupported feed type");
    }

    public static IFeedMatcher<T> CreateMatcher<T>() where T : TradeFeed
    {
        if (typeof(T) == typeof(DeltaOneFeed))
        {
            return (IFeedMatcher<T>)new DeltaOneFeedMatcher();
        }

        if (typeof(T) == typeof(EmFeed))
        {
            return (IFeedMatcher<T>)new EmFeedMatcher();
        }
        throw new ArgumentException("Unsupported feed type");
    }
}
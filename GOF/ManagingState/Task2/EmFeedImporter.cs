using ManagingState.Task2.Models;

namespace ManagingState.Task2;

public class EmFeedImporter
{
    private readonly IDatabaseRepository _repository;

    public EmFeedImporter(IDatabaseRepository repository)
    {
        _repository = repository;
    }

    public void Import(IEnumerable<EmFeed> feeds)
    {
        var validator = FeedFactory.CreateValidator<EmFeed>();
        var matcher = FeedFactory.CreateMatcher<EmFeed>();

        foreach (var feed in feeds)
        {
            var validationResult = validator.Validate(feed);
            if (validationResult.IsValid)
            {
                var existingFeeds = _repository.LoadFeeds<EmFeed>();
                if (!existingFeeds.Any(existingFeed => matcher.Match(feed, existingFeed)))
                {
                    _repository.SaveFeed(feed);
                }
            }
            else
            {
                _repository.SaveErrors(feed.StagingId, validationResult.ErrorMessages);
            }
        }
    }
}
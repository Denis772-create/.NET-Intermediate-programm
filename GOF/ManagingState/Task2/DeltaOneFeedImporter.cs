using ManagingState.Task2.Models;

namespace ManagingState.Task2;

public class DeltaOneFeedImporter
{
    private readonly IDatabaseRepository _repository;

    public DeltaOneFeedImporter(IDatabaseRepository repository)
    {
        _repository = repository;
    }

    public void Import(IEnumerable<DeltaOneFeed> feeds)
    {
        var validator = FeedFactory.CreateValidator<DeltaOneFeed>();
        var matcher = FeedFactory.CreateMatcher<DeltaOneFeed>();

        foreach (var feed in feeds)
        {
            var validationResult = validator.Validate(feed);
            if (validationResult.IsValid)
            {
                var existingFeeds = _repository.LoadFeeds<DeltaOneFeed>();
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
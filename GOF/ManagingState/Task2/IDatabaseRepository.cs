namespace ManagingState.Task2;

public interface IDatabaseRepository
{
    List<T> LoadFeeds<T>();

    void SaveFeed<T>(T feed);

    void SaveErrors(int feedStagingId, List<string> errors);
}

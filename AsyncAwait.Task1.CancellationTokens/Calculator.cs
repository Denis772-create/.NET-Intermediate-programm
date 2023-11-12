namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    // todo: change this method to support cancellation token
    public static async Task<long> CalculateAsync(int n, CancellationToken token)
    {
        long sum = 0;

        for (var i = 0; i < n; i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Sum calculation for {n} cancelled...");
                token.ThrowIfCancellationRequested(); // Propagate the cancellation
            }

            sum += (i + 1);
            await Task.Delay(10); // Simulate asynchronous operation
        }

        return sum;
    }
}
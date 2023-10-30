/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("Demonstrate the work of the each case with console utility.\n");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");

            var parentTask1 = Task.Run(() =>
            {
                Console.WriteLine($"Parent task #1 is doing some work in the thread #{Thread.CurrentThread.ManagedThreadId}...");
                Task.Delay(100);
                Console.WriteLine("Parent task #1 completed successfully.");
            });

            // a)
            var continuation1 = parentTask1.ContinueWith(_ =>
            {
                Console.WriteLine("Continuation task of parent task #1 executed regardless of the result of the parent task #1");
            });
            await Task.Delay(1000);

            // b)
            var continuation2 = parentTask1.ContinueWith(_ =>
            {
                Console.WriteLine($"Continuation task #{Task.CurrentId} executed when the parent task finished without success");
            }, TaskContinuationOptions.OnlyOnFaulted);
            await Task.Delay(1000);

            // c)
            var continuation3 = Task.Run(() =>
            {
                Console.WriteLine($"Parent task #2 is doing some work in the thread #{Thread.CurrentThread.ManagedThreadId}...");
                Task.Delay(100);
                throw new InvalidOperationException();
            })
            .ContinueWith(_ =>
            {
                Console.WriteLine($"Continuation task executed when the parent task #2 faulted and parent task thread #{Thread.CurrentThread.ManagedThreadId} is reused for continuation");

            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            await Task.Delay(1000);

            // d)
            var cts = new CancellationTokenSource();
            var continuation4 = Task.Run(async () =>
            {
                Console.WriteLine($"Parent task #3 is running. Is Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");
                await Task.Delay(1000, cts.Token);
            }, cts.Token)
            .ContinueWith(_ =>
            {
                Console.WriteLine($"Continuation executed when the parent task #3 was canceled. Is Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");
            }, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
            await Task.Delay(100);
            cts.Cancel();

            try
            {
                await Task.WhenAll(continuation1, continuation2, continuation3, continuation4);
            }
            catch
            {
                Console.ReadLine();
            }
        }
    }
}

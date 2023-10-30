/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    internal class Program
    {
        private static readonly Semaphore Semaphore = new Semaphore(0,1);

        static void Main()
        {
            int initialState = 10;

            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:\n");

            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            var firstThread = new Thread(DecrementAndPrintWithThreads);
            firstThread.Start(initialState);
            firstThread.Join();

            Console.WriteLine("\n- b) ThreadPool class for this task and Semaphore for waiting threads.");
            ThreadPool.QueueUserWorkItem(DecrementAndPrintWithThreadPool, initialState);
            Semaphore.WaitOne();

            Console.WriteLine("Main thread completed.");
            Console.ReadLine();
        }

        static void DecrementAndPrintWithThreads(object state)
        {
            var number = (int)state;
            if (number == 0) return;

            Console.WriteLine($"Thread ID {Thread.CurrentThread.ManagedThreadId}: {number}");

            var thread = new Thread(DecrementAndPrintWithThreads);
            thread.Start(--number);
            thread.Join();
        }

        static void DecrementAndPrintWithThreadPool(object state)
        {
            var number = (int)state;
            if (number == 0)
            {
                Semaphore.Release();
                return;
            }

            Console.WriteLine($"Thread ID {Thread.CurrentThread.ManagedThreadId}: {number}");

            ThreadPool.QueueUserWorkItem(DecrementAndPrintWithThreadPool, --number);
            Semaphore.WaitOne();

        }
    }
}

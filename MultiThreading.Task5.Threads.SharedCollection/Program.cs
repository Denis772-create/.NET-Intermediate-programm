/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection;

class Program
{
    static readonly object LockObject = new();
    static readonly List<int> SharedCollection = new();
    static async Task Main()
    {
        await Task.Run(AddElements);
        await Task.Run(PrintElements);
    }

    static void AddElements()
    {
        lock (LockObject)
        {
            for (var i = 1; i <= 10; i++)
            {
                SharedCollection.Add(i);
                Console.WriteLine($"Added: {i}");
            }
        }
    }

    static void PrintElements()
    {
        lock (LockObject)
        {
            if (SharedCollection.Count == 0)
                return;

            Console.WriteLine("Elements in the collection:");
            foreach (var element in SharedCollection)
                Console.WriteLine(element);
        }
    }
}
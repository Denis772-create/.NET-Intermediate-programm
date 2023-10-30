/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var random = new Random();

            Task.Run(() =>
            {
                var randomArray = Enumerable.Range(random.Next(100), 10).ToArray();
                Console.WriteLine($"Task #{Task.CurrentId}, Generated Array:");
                Console.WriteLine(string.Join(", ", randomArray));

                return randomArray;
            })
                .ContinueWith(firstTaskResult =>
                {
                    var array = firstTaskResult.Result;
                    var multiplier = random.Next(10);

                    Console.WriteLine($"Task #{Task.CurrentId}, Multiplied Array by {multiplier}:");
                    for (var i = 0; i < array.Length; i++)
                    {
                        array[i] *= multiplier;
                    }
                    Console.WriteLine(string.Join(", ", array));

                    return array;
                })
                .ContinueWith(secondTaskResult =>
                {
                    var array = secondTaskResult.Result;
                    Array.Sort(array);

                    Console.WriteLine("Sorted Array (Asc):");
                    Console.WriteLine(string.Join(", ", array));

                    return array;
                })
                .ContinueWith(thirdTaskResult =>
                {
                    var array = thirdTaskResult.Result;
                    Console.WriteLine($"Average Value: {array.Average():F2}");
                })
                .Wait();


            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace BittrexRxSharp.Helpers.Extensions
{
    public static class ObservableExtensions
    {
        /// <summary>
        /// Returns an observable sequence that produces a value after each period
        /// </summary>
        public static IObservable<TSource> IntervalTime<TSource>(this IObservable<TSource> source, int seconds)
        {
            return Observable.Interval(new TimeSpan(0, 0, seconds)).StartWith(0).Select(d => {
                var t = source.Select(data =>
                {
                    return data;
                });

                return t;

            }).Switch();
        }
    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Print the contents of a Enumerable to the console.
        /// </summary>
        public static void PrintAll<T>(this IEnumerable<T> source)
        {
            foreach (var obj in source)
                Console.WriteLine(JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented));
        }

        /// <summary>
        /// Prints the amount of elements specified to the console.
        /// </summary>
        /// <param name="amount"></param>
        public static void PrintSome<T>(this IEnumerable<T> @this, int amount = 10)
        {
            int count = 0;
            foreach (var obj in @this)
            {
                if (count < amount)
                    Console.WriteLine(JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented));
                count++;
            }
            Console.WriteLine("\n{0} elements of {1} were printed", amount, @this.Count());

        }

        /// <summary>
        /// Returns a string of the contents of the Enumerable.
        /// </summary>
        /// <param name="formating"></param>
        /// <returns></returns>
        public static string Stringify<T>(this IEnumerable<T> @this, Boolean formating = true)
        {
            return JsonConvert.SerializeObject(@this, formating ? Formatting.Indented : Formatting.None);
        }

    }
}

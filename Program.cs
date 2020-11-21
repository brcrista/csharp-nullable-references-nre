using System;
using System.Threading.Tasks;

namespace NullableReferencesNRE
{
    class Program
    {
        static async Task<TResult> ApplyAsync<T, TResult>(T obj, Func<T, Task<TResult>> func)
        {
            return await func(obj);
        }

        static async Task Main()
        {
            object? obj1 = null;
            object? obj2 = null;
            object? obj3 = null;
            object? obj4 = null;

            object result1 = await Task.FromResult(obj1);                               // Warning
            object result2 = await ApplyAsync(obj2, x => Task.FromResult(x));           // Warning
            object result3 = await ApplyAsync<object?, object>(obj3, Task.FromResult);  // Warning
            object result4 = await ApplyAsync(obj4, Task.FromResult);                   // No warning

            Console.WriteLine(result4.ToString()); // NRE
        }
    }
}

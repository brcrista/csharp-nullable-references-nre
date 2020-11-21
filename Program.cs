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
            object? obj = null;

            object result1 = await Task.FromResult(obj);                               // Warning
            object result2 = await ApplyAsync(obj, x => Task.FromResult(x));           // Warning
            object result3 = await ApplyAsync<object?, object>(obj, Task.FromResult);  // Warning
            object result4 = await ApplyAsync(obj, Task.FromResult);                   // No warning

            Console.WriteLine(result4.ToString()); // NRE
        }
    }
}

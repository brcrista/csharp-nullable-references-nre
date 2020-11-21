using System;
using System.Threading.Tasks;

namespace NullableReferencesNRE
{
    class Program
    {
        static async Task<TResult> ApplyAsync<T, TResult>(Task<T> task, Func<T, Task<TResult>> func)
        {
            return await func(await task);
        }

        static async Task Main()
        {
            var task1 = Task.FromResult<object?>(null);
            var task2 = Task.FromResult<object?>(null);
            var task3 = Task.FromResult<object?>(null);
            var task4 = Task.FromResult<object?>(null);

            object result1 = await Task.FromResult(await task1);                         // Warning
            object result2 = await ApplyAsync(task2, x => Task.FromResult(x));           // Warning
            object result3 = await ApplyAsync<object?, object>(task4, Task.FromResult);  // Warning
            object result4 = await ApplyAsync(task4, Task.FromResult);                   // No warning

            Console.WriteLine(result4.ToString()); // NRE
        }
    }
}

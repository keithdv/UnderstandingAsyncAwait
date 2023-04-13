// AsyncAwait keywords add readability

using System;

class Program
{
    // Asynchrony using Task and the Async/Await keywords
    // Asynchronous code reads like synchronous code
    static async Task<int> AsyncAwaitMethod()
    {
        Console.WriteLine("AsyncAwait Method Start");
        await Task.Delay(2000);
        Console.WriteLine("AsyncAwait Method Done");
        return 10;
    }

    static async Task<int> Main(string[] args)
    {
        var result = await AsyncAwaitMethod();
        Console.ReadLine();
        return result;
    }

    // Asynchrony using Task without the use of Async/Await keywords

    static Task<int> TaskMethod()
    {
        Console.WriteLine("TaskMethod Start");
        return Task.Delay(2000).ContinueWith(t =>
        {
            if(t.Exception != null) { throw  t.Exception; }
            Console.WriteLine("TaskMethod End");
            return 10;
        });
    }

    static Task<int> Main(string[] args)
    {
        return TaskMethod().ContinueWith(t =>
        {
            if (t.Exception != null) { throw t.Exception; }
            Console.ReadLine();
            return t.Result;
        });
    }



}
// AsyncAwait keywords add readability

async Task AsyncAwaitMethod()
{
    Console.WriteLine("AsyncAwait Method Start");
    await Task.Delay(2000);
    Console.WriteLine("AsyncAwait Method Done");
}

Task TaskMethod()
{
    Console.WriteLine("TaskMethod Start");
    return Task.Delay(1000).ContinueWith(t =>
    {
        Console.WriteLine("TaskMethod End");
    });
}

var tasks = new[] { AsyncAwaitMethod(), TaskMethod() };

await Task.Delay(3000);
await Task.WhenAll(tasks);

Console.ReadLine();
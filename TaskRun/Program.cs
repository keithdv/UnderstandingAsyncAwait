// Task.Run

using System.Collections.Concurrent;
using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();

ConcurrentBag<(int index, int threadId, long elapsedMilliseconds)> callInfoQueue 
    = new ConcurrentBag<(int index, int threadId, long)>();

async Task GetAsync(int index)
{
    Thread.Sleep(100); // Work
    callInfoQueue.Add((index, Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds));
    await Task.CompletedTask;
}

//Task GetAsync(int index)
//{
//    Thread.Sleep(100); // Work
//    callInfoQueue.Add((index, Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds));
//    return Task.CompletedTask;
//}

stopwatch.Start();

List<Task> tasks = new List<Task>();

for (int i = 0; i < 50; i++)
{
    var j = i;
    //tasks.Add(GetAsync(j)); // "Work" causes delay
    tasks.Add(Task.Run(() => GetAsync(j)));
};

await Task.WhenAll(tasks);

stopwatch.Stop();

callInfoQueue.OrderBy(x => x.index)
            .ThenBy(x => x.threadId).ToList()
            .ForEach(x => Console.WriteLine($"Thread: {x.threadId} Call:{x.index} Elapsed:{x.elapsedMilliseconds}"));

Console.WriteLine($"Runtime: {stopwatch.Elapsed.TotalMilliseconds}ms");

Console.ReadLine();



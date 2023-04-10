// AsyncPerformance

using System.Collections.Concurrent;
using System.Diagnostics;

var httpClient = new HttpClient();
Stopwatch stopwatch = new Stopwatch();

ConcurrentBag<(int index, int threadId, long elapsedMilliseconds)> callInfoQueue = new ConcurrentBag<(int index, int threadId, long)>();

async Task GetAsync(int index)
{
    Thread.Sleep(100); // Artificial Work
    callInfoQueue.Add((index, Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds));
    await Task.Delay(1000);
}

stopwatch.Start();

List<Task> tasks = new List<Task>();

for (int i = 0; i < 100; i++)
{
    var j = i;
    //tasks.Add(GetAsync(j)); // "Work" causes delay
    tasks.Add(Task.Run(() => GetAsync(j)));
};

await Task.WhenAll(tasks);

stopwatch.Stop();

Console.WriteLine($"Async {stopwatch.Elapsed.TotalMilliseconds}ms");

callInfoQueue.OrderBy(x => x.threadId).ThenBy(x => x.index).ToList().ForEach(x => Console.WriteLine($"Thread: {x.threadId} Call:{x.index} Elapsed:{x.elapsedMilliseconds}"));

Console.ReadLine();
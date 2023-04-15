// Make 100 server calls asynchronously
// Each server calls take 1 second

using System.Collections.Concurrent;
using System.Diagnostics;

var httpClient = new HttpClient();
ConcurrentBag<(int index, int threadId, long elapsedMilliseconds)> callInfoQueue 
    = new ConcurrentBag<(int index, int threadId, long)>();

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

// Key Code Block - Call Server 100 times asynchronously
async Task GetAsync(int index)
{
    callInfoQueue.Add((index, Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds));
    var message = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:5242/"));
    using HttpResponseMessage response = await httpClient.SendAsync(message); // Asynchronous Http Call
    if (!response.IsSuccessStatusCode) { throw new Exception("Failure"); }
}


List<Task> tasks = new List<Task>();

for (int i = 0; i < 100; i++)
{
    tasks.Add(GetAsync(i));
};

await Task.WhenAll(tasks);

// END Key Code Block

stopwatch.Stop();

callInfoQueue.OrderBy(x => x.elapsedMilliseconds)
    .ThenBy(x => x.index).ToList()
    .ForEach(x => Console.WriteLine($"Call:{x.index} Thread: {x.threadId} Elapsed:{x.elapsedMilliseconds}"));
Console.WriteLine($"Async Elapsed Time: {stopwatch.Elapsed.TotalMilliseconds}ms");

Console.ReadLine();
// Make 100 server calls in parallel
// Each server calls take 1 second

using System.Collections.Concurrent;
using System.Diagnostics;

// ThreadPool.SetMinThreads(100, 100); // Demonstrational purposes only. Not a solution!!

var httpClient = new HttpClient();
ConcurrentBag<(int index, int threadId, long elapsedMilliseconds)> callInfoQueue 
    = new ConcurrentBag<(int index, int threadId, long)>();

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

// Key Code Block - Call Server 100 times synchronously in parallel
void GetSync(int index)
{
    // Log the thread the call is made on
    callInfoQueue.Add((index, Thread.CurrentThread.ManagedThreadId, stopwatch.ElapsedMilliseconds));
    var message = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:5242/"));
    using HttpResponseMessage response = httpClient.Send(message); // Synchronous Http Call
    if (!response.IsSuccessStatusCode) { throw new Exception("Failure"); }
}

Parallel.For(0, 100, i =>
{
    GetSync(i);
});

// END Key Code Block

stopwatch.Stop();

callInfoQueue.OrderBy(x => x.elapsedMilliseconds)
    .ThenBy(x => x.threadId).ToList()
    .ForEach(x => Console.WriteLine($"Thread: {x.threadId} Call:{x.index} Elapsed:{x.elapsedMilliseconds}"));
Console.WriteLine($"Sync Elapsed Time: {stopwatch.Elapsed.TotalMilliseconds}ms");

Console.ReadLine();
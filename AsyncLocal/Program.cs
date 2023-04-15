// In an async flow where any thread can continue a task
// ThreadLocal no longer behaves as expected
// Use AsyncLocal

// NEVER do this!!!
// Breaks AsyncLocal; AsyncLocal is part of ExecutionContext
//ExecutionContext.SuppressFlow();

AsyncLocal<string> asyncLocal = new AsyncLocal<string>();
ThreadLocal<string> threadLocal = new ThreadLocal<string>();

asyncLocal.Value = "Blue";
threadLocal.Value = "Green";


async Task AsyncLocalVsThreadLocal()
{
    Console.WriteLine($"Before await: Thread-{Thread.CurrentThread.ManagedThreadId} AsyncLocal: {asyncLocal.Value} ThreadLocal: {threadLocal.Value}");
    await Task.Delay(100);
    // Different thread so ThreadLocal is empty
    Console.WriteLine($"After await: Thread-{Thread.CurrentThread.ManagedThreadId} AsyncLocal: {asyncLocal.Value} ThreadLocal: {threadLocal.Value}");
}

var t = AsyncLocalVsThreadLocal();

Thread.Sleep(500); // Force AsyncLocalVsThreadLocal to continue execution on a different thread

await t;

Console.ReadLine();



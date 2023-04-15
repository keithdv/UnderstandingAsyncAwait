


SynchronizationContext.SetSynchronizationContext(new MySynchronizationContext());

Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");
await Task.Delay(100); //.ConfigureAwait(false);
Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");

Console.ReadLine();

class MySynchronizationContext : SynchronizationContext
{
    public override void Post(SendOrPostCallback d, object? state)
    {
        Console.WriteLine("MySchronizationContext.Post");
        var aThread = new Thread(() => d(state));
        aThread.Start();
    }
}

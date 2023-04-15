// ExecutionContext continues the flow of critical context
// This shows you how CurrentCulture breaks without it

// NEVER do this!!! 
//ExecutionContext.SuppressFlow();

var currentCulture = Thread.CurrentThread.CurrentCulture;

async Task ChangeCultureToShowExecutionContext()
{
    Console.WriteLine($"Before await: Thread: {Thread.CurrentThread.ManagedThreadId} Culture: {Thread.CurrentThread.CurrentCulture.Name}");
    await Task.Delay(100);
    Console.WriteLine($"After await: Thread: {Thread.CurrentThread.ManagedThreadId} Culture: {Thread.CurrentThread.CurrentCulture.Name}"); // Different thread, wrong culture!
}

// Setting a specific CultureCode
Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");

var t = ChangeCultureToShowExecutionContext();

Thread.Sleep(500); // Force ChangeCultureToShowExecutionContext to continue execution on a different thread

await t;

Console.ReadLine();



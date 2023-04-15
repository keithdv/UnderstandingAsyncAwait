
// Lost exception due to Async Void (same as NoAwaitLostException)
// Only difference is the exception is written to the console
// Notice that there are no compile warnings or errors

async void LostException()
{
    await Task.Yield();
    throw new Exception("Waldo");
}

try
{
    LostException();

    Console.WriteLine("Completed No Exceptions");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"Exception Caught: {ex}");
    Console.ReadLine();
}



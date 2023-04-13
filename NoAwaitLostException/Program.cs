
// Lost exception due to not awaiting the task "t"
// Notice that there are no compile warnings or errors

async Task LostException()
{
    await Task.Yield();
    throw new Exception("Waldo");
}

try
{
    var myTask = LostException();

    Console.WriteLine("Completed No Exceptions");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex}");
    Console.ReadLine();
}


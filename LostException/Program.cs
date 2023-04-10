
// Lost exception due to not awaiting the task "t"
// Notice that there are no warnings / exceptions

var httpClient = new HttpClient();

async Task LostException()
{
    await Task.Delay(10);
    Console.WriteLine("Completed");
    throw new Exception("Failure"); // Where did it go?
}


var t = LostException();

await Task.Delay(100);

Console.ReadLine();

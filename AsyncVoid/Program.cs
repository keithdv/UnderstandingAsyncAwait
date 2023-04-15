

using System.Net.Http;

var c = SynchronizationContext.Current;
var httpClient = new HttpClient();

async void AsyncVoidMethod()
{
    await Task.Delay(1000);
    Console.WriteLine("Success!");
}

async void AsyncVoidMethodException()
{
    await Task.Delay(1000);
    throw new Exception("Find Waldo");
}


AsyncVoidMethod();
AsyncVoidMethodException();

// No Success Message or Error Without a Delay
 //await Task.Delay(2000);

Console.ReadLine();
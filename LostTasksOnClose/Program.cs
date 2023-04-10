// The tasks are not allowed to complete execution
// Notice that there are no warnings

void RunningTasks()
{
    for (int i = 0; i < 100; i++)
    {
        Task.Delay(i * 250).ContinueWith(t =>
        {
            var j = i;
            Console.WriteLine(j);
        });
    }
}

RunningTasks();

await Task.Delay(1000);
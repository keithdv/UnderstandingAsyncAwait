// Should write 1 to 100 to the console
// The tasks are not allowed to complete execution
// Notice that there are no warnings

void RunningTasks()
{
    for (int i = 0; i < 100; i++)
    {
        Task.Delay(i * 10).ContinueWith((t, j) =>
        {
            Console.WriteLine(j);
        }, i);
    }
}

RunningTasks();

await Task.Delay(100);
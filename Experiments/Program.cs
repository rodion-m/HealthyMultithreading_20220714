// See https://aka.ms/new-console-template for more information

var r = ThreadPool.SetMinThreads(2, 1000);
var r2 = ThreadPool.SetMaxThreads(2, 1000);
Console.WriteLine($"{r} {r2}");
AsyncLocal<int> v = new AsyncLocal<int>();
for (int i = 0; i < 10; i++)
{
    Task.Run(async () =>
    {
        Console.WriteLine($"{ThreadPool.ThreadCount}, {Environment.CurrentManagedThreadId}");
        v.Value = Task.CurrentId.Value;
        await Task.Delay(500);
        Console.WriteLine($"{v.Value}, {Environment.CurrentManagedThreadId}");
    });
}
await Task.Delay(10000);
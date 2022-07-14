using System.Diagnostics;
using AsyncIO;
using ThreadVsTask;
using ThreadVsTask.Workers;

/*
 * 86 (95 with browsers) Context Switches in empty app (without *Work.Run): https://i.imgur.com/hyL1D2D.png
 *
 * OneThreadWork.Run: 104 CS, 5668 ms [1000 * 10_000_000]: https://i.imgur.com/DNZdACq.png
 *    5668 / 18 = 314 ms without context switch
 * 
 * ParallelForWork.Run: 2500-3700 CS, 736-840 ms [10_000 * 1_000_000]: https://i.imgur.com/ZHIWt5m.png
 * 
 * TasksWork.Run: 2500-6000 CS, 736-840 ms [10_000 * 1_000_000]: https://i.imgur.com/Mzh5OxM.png
 *
 * NativeThreadsWork.Run 23-26k CS, 2000-2270 ms [10_000 * 1_000_000]: https://i.imgur.com/7PST7VS.png
 * 
 * ThreadPoolWork.Run: 2100-5000 CS, 765-832 ms [1000 * 10_000_000]
 *
 * NativeThreadsWork.CreateThreadsSerialAndJoin(10_000): 30k CS, 3168 ms https://i.imgur.com/fU3QDnd.png
 *    250k тактов CPU на 1 CS
 *    760k тактов CPU на создание и уничтожение одно потока
 *    MainThread: 10k CS: https://i.imgur.com/QF3UzqD.png
 *
 * NativeThreadsWork.CreateThreadsAndWait(10_000): 20k CS, 2407 ms https://i.imgur.com/jl5KB2x.png
 *    360k тактов CPU на 1 CS
 *    730к тактов CPU на создание и вызов WaitOne в одном потоке
 *    MainThread: 200-500 CS: https://i.imgur.com/Zedi3TW.png
 *    почти у каждого нового треда по 2 CS
 *    после вызова .Set() пробуждение и уничтожение потоков провоцирует 2.5kk CS и занимает 2:43 CPU TIME: https://i.imgur.com/Zedi3TW.png
 *
 * NativeThreadsWork.CreateThreadsSerial(10_000): 20k CS, 2000 ms https://i.imgur.com/fe9PqJx.png
 *    MainThread: 150 CS: https://i.imgur.com/nWRTK13.png
 */

var sw = Stopwatch.StartNew();
// for (var i = 0L; i < 3_500_000_000L; i++) //2087, 165
// {
//     //Thread.Sleep(0); //412 мс
//     //Thread.Yield(); //207 мс
// }
//NativeThreadsWork.CreateThreads();
//var e = NativeThreadsWork.CreateThreadsAndWait();

//ExecutionContext.SuppressFlow();

//OneThreadWork.Run();
// CompletionPort completionPort = CompletionPort.Create();
// while (true)
// {
//  var b = completionPort.GetQueuedCompletionStatus(100, out var status);
// }

//ParallelForWork.Run();
//NativeThreadsWork.Run();
// ThreadPool.SetMinThreads(8, 1000);
// ThreadPool.SetMaxThreads(8, 1000);
await TasksWork.RunAsync();
sw.Stop();

Console.WriteLine("Done " + sw.ElapsedMilliseconds);
Console.ReadKey();
sw.Restart();
//e.Set();
Console.WriteLine("Done " + sw.ElapsedMilliseconds);
Console.ReadKey();
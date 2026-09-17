using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

void StressTest()
{
    long count = 0;
    MapService mapService = new MapService();

    try
    {
        while (true)
        {
            mapService.AddPoi($"Точка {count}", new Coordinate(55.75, 37.61));

            count++;

            if (count % 100_000 == 0)
            {
                long memoryMb = GC.GetTotalMemory(false) / 1024 / 1024;

                Console.WriteLine($"Точек: {count:N0} | RAM: {memoryMb} MB");
            }
        }
    }

    catch (Exception ex)
    {
        Console.WriteLine($"{ex.GetType().Name} на {count:N0} точках");
        Console.WriteLine($"Сообщение ошибки: {ex.Message}");
    }
}

void ConcurrencyTest()
{
    MapService mapService = new MapService();
    int errors = 0;

    //HashSet<string> threadId = new HashSet<string>();

    try
    {
        Parallel.For(0, 10, i =>
        {
            for (int j = 0; j < 1_000; j++)
            {
                //threadId.Add($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");

                mapService.AddPoi($"Thread Id: {Thread.CurrentThread.ManagedThreadId} Точка {i}-{j}",
                    new Coordinate(55.75, 37.61));
            }
        });

    }

    catch (AggregateException ex)
    {
        foreach (var inner in ex.InnerExceptions)
        {
            Console.WriteLine($"Ошибка: {inner.GetType().Name}");
            Console.WriteLine($"  {inner.Message}");

            errors++;
        }
    }

    //finally
    //{
    //    foreach (var i in threadId)
    //    { 
    //        Console.WriteLine(i);
    //    }
    //}

    Console.WriteLine($"Итоговое количество точек: {mapService.All.Count}");
    Console.WriteLine($"Ожидалось: 10 000");
    Console.WriteLine($"Ошибок: {errors}");
}

void ConcurrencyTestThreadSafe()
{
    MapServiceThreadSafe mapService = new MapServiceThreadSafe();
    int errors = 0;

    try
    {
        Parallel.For(0, 10, i =>
        {
            for (int j = 0; j < 1_000; j++)
            {
                mapService.AddPoi($"Thread Id: {Thread.CurrentThread.ManagedThreadId} Точка {i}-{j}",
                    new Coordinate(55.75, 37.61));
            }
        });

    }

    catch (AggregateException ex)
    {
        foreach (var inner in ex.InnerExceptions)
        {
            Console.WriteLine($"Ошибка: {inner.GetType().Name}");
            Console.WriteLine($"  {inner.Message}");

            errors++;
        }
    }

    Console.WriteLine($"Итоговое количество точек: {mapService.All.Count}");
    Console.WriteLine($"Ожидалось: 10 000");
    Console.WriteLine($"Ошибок: {errors}");
}

//ConcurrencyTest();
ConcurrencyTestThreadSafe();
//StressTest();
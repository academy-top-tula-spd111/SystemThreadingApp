using System.Threading;

int num = 1;

object locker = new();

Mutex mutex = new Mutex();

for(int i = 0; i < 5; i++)
{
    Thread thread = new(PrintNum);
    thread.Name = $"Thread {i + 1}";
    thread.Start();
}

//Console.WriteLine($"itog: {num}");


void PrintNum()
{
    bool isLock = false;
    for (int i = 0; i < 10; i++)
    {
        //mutex.WaitOne();
        try
        {
            isLock = false;
            Monitor.Enter(locker, ref isLock);
            Console.WriteLine($"{Thread.CurrentThread.Name}: {num}");
            ++num;
            Thread.Sleep(100);
        }
        finally
        {
            if(isLock) Monitor.Exit(locker);
        }
        
        //mutex.ReleaseMutex();

        //lock(locker)
        //{
        //    Console.WriteLine($"{Thread.CurrentThread.Name}: {num}");
        //    ++num;
        //    Thread.Sleep(100);
        //}
    }


}

void ThreadCounter()
{
    Thread thread = new Thread(Counter);
    thread.Start(20);

    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"main thread {i}");
        Thread.Sleep(200);
    }

    void Counter(object max)
    {
        for (int i = 0; i < (int)max; i++)
        {
            Console.WriteLine($"child thread {i}");
            Thread.Sleep(300);
        }
    }
}
void ThreadConstruct()
{
    Thread thread1 = new Thread(Hello);
    Thread thread2 = new Thread(new ThreadStart(Hello));
    Thread thread3 = new Thread(() => Console.WriteLine("Hello World"));

    thread1.Start();
    thread2.Start();
    thread3.Start();

    void Hello() => Console.WriteLine("Hello World");
}
void ThreadWelcome()
{
    Thread thread = Thread.CurrentThread;
    Console.WriteLine($"Name: {thread.Name}");
    thread.Name = "Test";
    Console.WriteLine($"Name: {thread.Name}");

    Console.WriteLine($"Is Alive: {thread.IsAlive}");
    Console.WriteLine($"Id: {thread.ManagedThreadId}");
    Console.WriteLine($"Priority: {thread.Priority}");
    Console.WriteLine($"Status of thread: {thread.ThreadState}");

    Console.WriteLine($"Domain: {Thread.GetDomain()}");
    Console.WriteLine($"Domain: {Thread.GetDomainID()}");

    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(1000);
        Console.WriteLine(i);
    }
}

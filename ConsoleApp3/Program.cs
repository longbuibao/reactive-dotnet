using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConnectableObservable<long> connectableTicks = Observable.Timer(dueTime: TimeSpan.Zero, period: TimeSpan.FromSeconds(1)).Replay();
            connectableTicks.Connect();
            IObserver<long> firstObserver = Observer.Create<long>((x) =>
            {
                Console.WriteLine($"First Observer: {x}");
                if (x == 5)
                {
                    Console.WriteLine("First Observer completed");
                }
            });
            IObserver<long> secondObserver = Observer.Create<long>((x) =>
            {
                Console.WriteLine($"Second Observer: {x}");
            });
            connectableTicks.Subscribe(firstObserver);
            Observable.Timer(TimeSpan.FromSeconds(6)).Subscribe(_ =>
            {
                Console.WriteLine("Second Observer subscribing");
                connectableTicks.Subscribe(secondObserver);
            });
            Console.ReadLine();
        }
    }
}

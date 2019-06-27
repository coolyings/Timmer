using System;
using System.Threading;
using System.Threading.Tasks;

namespace TreadTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var task = new Task(() =>
              {
                  for (int i = 0; i < 20; i++)
                  {
                      Thread.Sleep(10);
                      Console.WriteLine("子线程");
                  }
              });
            task.Start();
            Thread.Sleep(10);
            Console.WriteLine("主线程");
            Console.ReadKey();
        }
    }
}

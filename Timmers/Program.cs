using System;
using System.ServiceProcess;
using System.Timers;

namespace Timmers
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var timmer = new System.Timers.Timer()
            {
                Enabled = true,
                Interval = 60000
            };
            timmer.Start();
            timmer.Elapsed += Excute;
            Console.ReadKey();
        }

        private static void Excute(object sender, ElapsedEventArgs e)
        {
            //if (DateTime.Now.Day / 3 == 0 && DateTime.Now.Hour == 3 && DateTime.Now.Minute == 0)
            if (DateTime.Now.Hour == 3 && DateTime.Now.Minute == 0)
            {
                ServiceController[] service = ServiceController.GetServices();

                for (int i = 0; i < service.Length; i++)
                {
                    if (service[i].DisplayName == "syyk.android.api" || service[i].DisplayName == "syyk.app.api" || service[i].DisplayName == "syyk.signalr.api" || service[i].DisplayName == "syyk.live.api")
                    {
                        if (service[i].Status == ServiceControllerStatus.Running)
                        {
                            service[i].Stop();
                            ServiceLog.Logger.Information(service[i].DisplayName);
                        }
                    }
                }
            }
        }

    }
}

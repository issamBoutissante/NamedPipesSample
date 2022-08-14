using NamedPipesSample.AudrosAPI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.AudrosAPI.Host
{
    public static class ServiceRunner
    {
		public static List<ServiceHost> RunningServices { get; set; } = new List<ServiceHost>();
        public static void Run(List<IService> services)
        {
			if (!Environment.UserInteractive)
			{
				foreach (IService service in services)
				{
					using (ServiceHost serviceHost = new ServiceHost(service.GetType().ToString(), service))
                    {
						ServiceBase.Run(serviceHost);
						RunningServices.Add(serviceHost);
                    }
				}
			}
			else
			{
				List<ServiceHost> serviceHosts = new List<ServiceHost>();
				foreach (IService service in services)
				{
					ServiceHost serviceHost = new ServiceHost(service.GetType().ToString(), service);
					serviceHost.Run();
					serviceHosts.Add(serviceHost);
					Console.WriteLine($"{service.GetType().ToString()} Started.");
					RunningServices.Add(serviceHost);
				}
			}

		}
		public static void Stop() => RunningServices.ForEach(serviceHost => serviceHost?.Abort());
	}
}

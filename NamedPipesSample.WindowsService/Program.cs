using NamedPipesSample.Common;
using NamedPipesSample.WindowsService;
using System.ServiceProcess;

Console.WriteLine("Starting Service...");

List<IService> services = new List<IService>()
{
	new SayHiService(),
	new SayBuyService()
};

if (!Environment.UserInteractive)
{
	foreach (IService service in services)
	{
		using (ServiceHost serviceHost = new ServiceHost(service.GetType().ToString(), service))
			ServiceBase.Run(serviceHost);
	}
}
else
{
	List<ServiceHost> serviceHosts = new List<ServiceHost>();
	foreach(IService service in services)
    {
		ServiceHost serviceHost = new ServiceHost(service.GetType().ToString(), service);
		serviceHost.Run(args);
		serviceHosts.Add(serviceHost);
		Console.WriteLine($"{service.GetType().ToString()} Started.");
	}
	Console.WriteLine("Press ESC to stop...");
	while (Console.ReadKey(true).Key != ConsoleKey.Escape)
	{

	}
	foreach (ServiceHost serviceHost in serviceHosts)
	{
		serviceHost.Abort();
	}
}

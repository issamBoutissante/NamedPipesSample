using NamedPipesSample.AudrosAgentService.Services;
using NamedPipesSample.AudrosAPI;
using NamedPipesSample.AudrosAPI.Contract;
using NamedPipesSample.AudrosAPI.Host;
using System.ServiceProcess;

Console.WriteLine("Starting Service...");

List<IService> services = new List<IService>()
{
	new SayHiService(),
	new SayBuyService()
};
ServiceRunner.Run(services);
Console.WriteLine("Press ESC to stop...");
while (Console.ReadKey(true).Key != ConsoleKey.Escape)
{

}
ServiceRunner.Stop();
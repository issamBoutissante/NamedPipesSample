using NamedPipesSample.AudrosAPI.Contract;
using NamedPipesSample.AudrosAPI.Server;
using System.ServiceProcess;

namespace NamedPipesSample.AudrosAPI.Host
{
    public class ServiceHost:ServiceBase
    {
        private Thread serviceThread;
        private bool stopTread;
        public ServiceHost(string pipeName, IService hostedService)
        {
            new NamedPipesServer(pipeName,hostedService);
            this.ServiceName = $"{pipeName} Named Pipe";
        }
        protected override void OnStart(string[] args) => Run();
        protected override void OnStop() => Abort();
        protected override void OnShutdown() => Abort();
        public void Run()
        {
            serviceThread = new Thread(InitializeServiceThread)
            {
                Name = "Named Pipes Sample Service Thread",
                IsBackground = true
            };
            serviceThread.Start();
        }
        public void Abort() => stopTread = true;
        private void InitializeServiceThread()
        {
            while (!stopTread)
            {
                Task.Delay(100).GetAwaiter().GetResult();
            }
        }
    }
}

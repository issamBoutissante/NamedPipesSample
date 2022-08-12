using NamedPipesSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.WindowsService
{
    public class ServiceHost:ServiceBase
    {
        private Thread serviceThread;
        private bool stopping;
        private NamedPipesServer pipeServer;
        public ServiceHost(string pipeName, IService hostedService)
        {
            pipeServer = new NamedPipesServer(pipeName,hostedService);
            this.ServiceName = "My Sample Windows Service";
        }
        protected override void OnStart(string[] args)
        {
            Run(args);
        }
        protected override void OnStop()
        {
            Abort();
        }
        protected override void OnShutdown()
        {
            Abort();
        }
        public void Run(string[] args)
        {
            serviceThread = new Thread(InitializeServiceThread)
            {
                Name = "Named Pipes Sample Service Thread",
                IsBackground = true
            };
            serviceThread.Start();
        }
        public void Abort()
        {
            stopping = true;
        }
        private void InitializeServiceThread()
        {
            while (!stopping)
            {
                Task.Delay(100).GetAwaiter().GetResult();
            }
        }
    }
}

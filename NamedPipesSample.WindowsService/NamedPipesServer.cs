using H.Pipes;
using H.Pipes.Args;
using NamedPipesSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.WindowsService
{
    public class NamedPipesServer: IDisposable
    {

        private PipeServer<PipeMessage> server;
        public IService service { get; set; }
        public NamedPipesServer(string pipeName,IService hostedService)
        {
            this.service = hostedService;
            InitializeAsync(pipeName).Wait();
        }
        private async Task InitializeAsync(string pipeName)
        {
            server = new PipeServer<PipeMessage>(pipeName);

            server.MessageReceived += (sender, args) => OnMessageReceived(args);
            server.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);

            await server.StartAsync();
        }
        private void OnMessageReceived(ConnectionMessageEventArgs<PipeMessage> args)
        {
            if (args.Message.GetType() == typeof(Request))
            {
                Request request = (Request)args.Message;
                Response response = service.SendRequest(request);
                args.Connection.WriteAsync(response);
            }
        }
        private void OnExceptionOccurred(Exception ex)
        {
            Console.WriteLine($"Exception occured in pipe: {ex}");
        }
        public void Dispose()
        {
            DisposeAsync().GetAwaiter().GetResult();
        }
        public async Task DisposeAsync()
        {
            if (server != null)
                await server.DisposeAsync();
        }
    }
}

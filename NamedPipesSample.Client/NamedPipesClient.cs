using H.Pipes;
using NamedPipesSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.Client
{
    public class NamedPipesClient : IDisposable
    {
        private PipeClient<PipeMessage> Client;
        public static NamedPipesClient CreateClient(string pipeName)
        {
            NamedPipesClient client = new NamedPipesClient();
            client.Initialize(pipeName);
            return client;
        }
        public void Initialize(string pipeName)
        {
            if (Client != null && Client.IsConnected)
                return;

            Client = new PipeClient<PipeMessage>(pipeName);
            Client.MessageReceived += (sender, args) => OnMessageReceived(args.Message);
            Client.Connected += (o, args) => Console.WriteLine($"Connected to {pipeName}");
            Client.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);

            Client.ConnectAsync().Wait();
        }
        public Response Response { get; set; }
        public async Task<Response> SendRequest(Request request)
        {
            await Client.WriteAsync(new Request()
            {
                RequestText=request.RequestText
            });
            while (Response == null)
            {
                //This Wile Loop will wait for OnMessageReceived To Be Invoked To Fill The Response
            }
            Response response = this.Response;
            this.Response = null;
            return response;
        }
        private void OnMessageReceived(PipeMessage message)
        {
            if (message.GetType() == typeof(Response))
            {
                this.Response = message as Response;
            }
        }
        private void OnExceptionOccurred(Exception exception)
        {
            Console.WriteLine($"An exception occured: {exception}");
        }
        public void Dispose()
        {
            if (Client != null)
                Client.DisposeAsync().GetAwaiter().GetResult();
        }
    }
}

using H.Pipes;
using H.Pipes.Args;
using NamedPipesSample.AudrosAPI.Contract;

namespace NamedPipesSample.AudrosAPI.Server
{
    public class NamedPipesServer: IDisposable
    {

        private PipeServer<PipeMessage> Server;
        public IService HostedService { get; set; }
        public NamedPipesServer(string pipeName,IService hostedService)
        {
            this.HostedService = hostedService;
            InitializeAsync(pipeName).Wait();
        }
        private async Task InitializeAsync(string pipeName)
        {
            Server = new PipeServer<PipeMessage>(pipeName);
            Server.MessageReceived += (sender, args) => OnMessageReceived(args);
            Server.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);
            await Server.StartAsync();
        }
        private void OnMessageReceived(ConnectionMessageEventArgs<PipeMessage?> args)
        {
            Request request = (Request)args.Message!;
            Response response = HostedService.SendRequest(request);
            args.Connection.WriteAsync(response);
        }
        private void OnExceptionOccurred(Exception ex)
        {
            //TODO : We May Need To Write Exception On The Log File
        }
        public void Dispose()
        {
            DisposeAsync().GetAwaiter().GetResult();
        }
        public async Task DisposeAsync()
        {
            if (Server != null)
                await Server.DisposeAsync();
        }
    }
}

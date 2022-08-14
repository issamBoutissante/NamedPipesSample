using H.Pipes;
using NamedPipesSample.AudrosAPI.Contract;

namespace NamedPipesSample.AudrosAPI.Client;
public class NamedPipesClient : IDisposable
{
    private PipeClient<PipeMessage> Client;
    public static NamedPipesClient CreateClient(string pipeName, int timeout=10000)
    {
        NamedPipesClient client = new NamedPipesClient();
        client.Initialize(pipeName, timeout);
        return client;
    }
    public void Initialize(string pipeName,int timeout)
    {
        if (Client != null && Client.IsConnected)
            return;

        Client = new PipeClient<PipeMessage>(pipeName);
        Client.MessageReceived += (sender, args) => OnMessageReceived(args.Message);
        Client.Connected += (o, args) => Console.WriteLine($"Connected to {pipeName}");
        Client.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);

        Client.ConnectAsync().Wait(timeout);
        if (!Client.IsConnected)
            throw new Exception("Coudn't Connect To Server");
    }
    #region This Block Of Code Will Work Together To send request and rturn A Response
    public Response? Response { get; set; }
    public async Task<Response> SendRequest(Request request)
    {
        if (!Client.IsConnected)
            throw new Exception("Client Is Not Connected");
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
    #endregion
    private void OnExceptionOccurred(Exception exception)
    {
        //TODO : We May Need To Write Exception On The Log File
    }
    public void Dispose() => Client?.DisposeAsync().GetAwaiter().GetResult();
}

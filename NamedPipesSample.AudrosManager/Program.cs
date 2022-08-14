using NamedPipesSample.AudrosAPI.Client;
using NamedPipesSample.AudrosAPI.Contract;

do
{
    try
    {
        Console.Clear();
        Console.WriteLine("Enter The Pipe Name That You Want To Request From : ");
        string pipeName = Console.ReadLine();
        var Client = NamedPipesClient.CreateClient(pipeName,3000);
        Console.WriteLine("Enter The The Request Text : ");
        string requestText = Console.ReadLine();
        Response response = await Client.SendRequest(new Request()
        {
            RequestText = requestText
        });
        Console.WriteLine(response.ResponseText);
    }catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    Console.WriteLine("\n\n.....Press Any Key To Retry");
    Console.WriteLine(".....Press Escape To Stop");
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

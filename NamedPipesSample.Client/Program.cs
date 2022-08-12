using NamedPipesSample.Client;
using NamedPipesSample.Common;

do
{
    Console.WriteLine("Enter The Pipe Name That You Want To Request From : ");
    string pipeName = Console.ReadLine();
    var Client = NamedPipesClient.CreateClient(pipeName);
    Console.WriteLine("Enter The The Request Text : ");
    string requestText = Console.ReadLine();
    Response response = await Client.SendRequest(new Request()
    {
        RequestText = requestText
    });
    Console.WriteLine(response.ResponseText);
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

using NamedPipesSample.AudrosAPI;
using NamedPipesSample.AudrosAPI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.AudrosAgentService.Services
{
    public class SayHiService : IService
    {
        public Response SendRequest(Request request)
        {
            Console.WriteLine(request.RequestText);
            return new Response()
            {
                ResponseText = $"This A Response from SAyHiService - Type : {this.GetType()}"
            };
        }
    }
}

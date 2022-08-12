using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.Common
{
    [Serializable]
    public class Response:PipeMessage
    {
        public Guid Id { get; set; }
        public string ResponseText { get; set; }
        public Response()
        {
            Id = Guid.NewGuid();
        }
    }
}

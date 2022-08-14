using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.AudrosAPI.Contract
{
    [Serializable]
    public class Request:PipeMessage
    {
        public Guid Id { get; set; }
        public string RequestText { get; set; }
        public Request()
        {
            Id = Guid.NewGuid();
        }
    }
}

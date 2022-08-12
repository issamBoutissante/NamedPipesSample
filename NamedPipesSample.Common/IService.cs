using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.Common
{
    public interface IService
    {
        Response SendRequest(Request request);
    }
}

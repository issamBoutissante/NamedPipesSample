﻿using NamedPipesSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesSample.WindowsService
{
    public class SayBuyService:IService
    {
        public Response SendRequest(Request request)
        {
            Console.WriteLine(request.RequestText);
            return new Response()
            {
                ResponseText = $"This A Response from SayBuyService - Type : {this.GetType()}"
            };
        }
    }
}
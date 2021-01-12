using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit_tech_test.Messages
{
    public class InitializeRequest
    {

    }
    public class InitializeResponse : BaseMessageResponse
    {
        public string Message { get; set; }
    }
}

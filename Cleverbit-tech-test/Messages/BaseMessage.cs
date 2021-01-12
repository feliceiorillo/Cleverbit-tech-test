using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit_tech_test.Messages
{
    public class BaseMessageResponse
    {

        public bool IsSuccess { get; set; }
        public string Exception { get; set; }
        public void SetException(Exception ex)
        {
            IsSuccess = false;
            Exception = ex.Message;
        }



    }
}

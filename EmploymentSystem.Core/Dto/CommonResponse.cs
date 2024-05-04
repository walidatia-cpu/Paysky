using EmploymentSystem.Core.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Core.Dto
{
    public class CommonResponse<T> where T : class
    {
        public RequestStatus RequestStatus { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object ModelError { get; set; }
    }
}

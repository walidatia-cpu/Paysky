using EmploymentSystem.Core.Constant;

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

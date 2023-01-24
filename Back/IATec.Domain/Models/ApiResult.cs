namespace IATec.Domain.Models
{
    public sealed class ApiResult
    {
        public object Result { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
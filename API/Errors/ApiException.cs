namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statuscode, string message = null , string details = null)
        {
            this.StatusCode = statuscode;
            this.Message = message;
            this.Details = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
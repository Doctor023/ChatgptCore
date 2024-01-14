namespace ChatgptCore.Models
{
    public class ErrorBadRequestModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ErrorBadRequestModel(string message)
        {
            StatusCode = 400;
            Message = message;
        }
    }
}

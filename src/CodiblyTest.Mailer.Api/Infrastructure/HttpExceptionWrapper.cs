namespace CodiblyTest.Mailer.Api.Infrastructure
{
    public class HttpExceptionWrapper
    {
        public HttpExceptionWrapper(int statusCode, string error)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public int StatusCode { get; }

        public string Error { get; }
    }
}
using System.Net;

namespace CodiblyTest.Mailer.Api.Infrastructure
{
    public class HttpStatusCodeInfo
    {
        public HttpStatusCodeInfo(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public HttpStatusCode Code { get; }
        public string Message { get; }

        public static HttpStatusCodeInfo Create(HttpStatusCode code, string message)
        {
            return new HttpStatusCodeInfo(code, message);
        }
    }
}
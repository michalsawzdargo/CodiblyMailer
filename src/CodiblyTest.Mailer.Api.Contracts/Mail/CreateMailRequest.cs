using System;
using System.Collections.Generic;
using System.Text;

namespace CodiblyTest.Mailer.Api.Contracts.Mail
{
    public class CreateMailRequest
    {
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Priority { get; set; }
    }
}

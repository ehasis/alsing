using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Commands.Services
{
    public class DefaultEmailService : IEmailService
    {
        #region IEmailService Members

        public void SendEmail(string recipients, string subject, string body)
        {
            //send the email

            Console.WriteLine("{0} {1} {2}", recipients, subject, body);
        }

        #endregion
    }
}

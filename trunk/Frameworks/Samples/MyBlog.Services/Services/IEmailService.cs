using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Commands.Services
{
    public interface IEmailService
    {
        void SendEmail(string recipients, string subject, string body);
    }
}

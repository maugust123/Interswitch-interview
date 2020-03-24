using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApi.Common.MailBase
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string receipientEmail, string subject, string body, AttachmentModel attachment = null,
            bool isHtmlBody = false, string displayName = "Interview app",
            string bcc = "", string cc = "");
    }
}

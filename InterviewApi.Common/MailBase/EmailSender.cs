using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using static InterviewApi.Common.UserMessages;

namespace InterviewApi.Common.MailBase
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : ReadConfig, IEmailSender
    {
        //http://jameschambers.com/2016/01/Configuration-in-ASP-NET-Core-MVC/
        //https://hassantariqblog.wordpress.com/2017/03/20/asp-net-core-sending-email-with-gmail-account-using-asp-net-core-services/
        //https://www.codeproject.com/Articles/1166364/Send-email-with-Net-Core-using-Dependency-Injectio
        //https://www.humankode.com/asp-net-core/asp-net-core-configuration-best-practices-for-keeping-secrets-out-of-source-control
        //https://stackoverflow.com/questions/30824303/how-to-configure-email-settings-in-asp-net-5

        public EmailSender(IOptions<AppSecretsConfig> options) : base(options) { }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="receipientEmail">A string of email address separated with \';\'</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email message</param>
        /// <param name="attachment">Attachment full name</param>
        /// <param name="isHtmlBody">Specify if the email IsBodyHtml</param>
        /// <param name="displayName">The string that contains the display name associated with the address</param>
        /// <param name="bcc">A string of email address separated with ';'</param>
        /// <param name="cc">A string of email address separated with ';'</param>
        public async Task<bool> SendEmailAsync(string receipientEmail, string subject, string body, AttachmentModel attachment = null,
            bool isHtmlBody = false, string displayName = "Interview", string bcc = "", string cc = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receipientEmail)) throw new ApplicationException(EmptyEmailAddress);

                using (var client = new SmtpClient(HostName, PortNumber))
                {
                    //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                    client.Credentials = new NetworkCredential(UserName, Password);
                    client.EnableSsl = true;

                    var message = new MailMessage
                    {
                        From = new MailAddress(MailSender, displayName, Encoding.UTF8),
                        Subject = subject.Replace('\r', ' ').Replace('\n', ' '),
                        SubjectEncoding = Encoding.UTF8,
                        Body = body,
                        BodyEncoding = Encoding.UTF8,
                        IsBodyHtml = isHtmlBody,
                        Priority = MailPriority.High,
                        //ReplyToList = { new MailAddress(MailSender) },
                        DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                    };

                    receipientEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                        .ForEach(email => message.To.Add(new MailAddress(email)));
                    bcc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                        .ForEach(email => message.Bcc.Add(new MailAddress(email)));
                    cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                        .ForEach(email => message.CC.Add(new MailAddress(email)));
                    if (attachment != null)
                    {
                        if (attachment.EnableAttachment && !string.IsNullOrWhiteSpace(attachment.AttachmentPath))
                        {
                            message.Attachments.Add(new Attachment(attachment.AttachmentPath));
                        }
                        else if (attachment.EnableAttachment && attachment.AttachmentStream != null)
                        {
                            message.Attachments.Add(new Attachment(attachment.AttachmentStream, attachment.StreamName,
                                attachment.ContentType));
                        }
                    }

                    await client.SendMailAsync(message);
                    return true;
                }

            }
            catch (SmtpFailedRecipientException smtp)
            {
                Console.WriteLine(smtp.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}

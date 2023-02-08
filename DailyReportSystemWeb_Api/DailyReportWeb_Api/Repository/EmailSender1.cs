using DailyReportWeb_Api.Repository.IRepository;
using DailyReportWeb_Api.Utility;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Repository
{
    public class EmailSender1: IEmailSender
    {
        private EmailSettings _emailSettings { get; }
        public EmailSender1(IOptions<EmailSettings> emailsettings)
        {
            _emailSettings = emailsettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
                {
                    string toemail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;
                      MailMessage mail = new MailMessage()
                      {
                      From = new MailAddress(_emailSettings.UserNameEmail, "Daily Report System")
                      };
                    mail.To.Add(new MailAddress(toemail));
                    mail.CC.Add(new MailAddress(_emailSettings.CCEmail));
                    mail.Subject = "Daily Report System";
                    mail.Body = htmlMessage;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                    {
                        smtp.Credentials = new NetworkCredential(_emailSettings.UserNameEmail, _emailSettings.UserNamePassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                    }
                           catch (Exception ex)
                    {
                           // Handle the exception here
                               string str = ex.Message;
                    }
            }
    }
}

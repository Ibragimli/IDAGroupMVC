using IDAGroupMVC.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Services
{
    public class EmailServices
    {

        public interface IEmailService
        {
            void Send(string to, string subject, string html);
        }

        public class EmailService : IEmailService
        {
            private readonly DataContext _context;

            public EmailService(DataContext context)
            {
                _context = context;
            }
            public void Send(string to, string subject, string html)
            {
                var context = _context.EmailSetting.FirstOrDefault(x => x.Id == 1);

                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(context.SmtpEmail));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(context.SmtpHost, context.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(context.SmtpEmail, context.SmtpPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}

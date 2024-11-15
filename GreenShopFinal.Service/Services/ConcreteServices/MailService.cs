using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string to, string subject, string body)
        {
            string from = _configuration["Mail:from"];

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject=subject;
            mail.Body=body;
            mail.From=new MailAddress(_configuration["Mail:From"]);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = _configuration["Smtp:Host"];
            smtpClient.Port = Convert.ToInt32(_configuration["Smtp:Port"]);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(from, _configuration["Smtp:Password"]);
            smtpClient.EnableSsl = true;

            smtpClient.Send(mail);
        }
    }
}

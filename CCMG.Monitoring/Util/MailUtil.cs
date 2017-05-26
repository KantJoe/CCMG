using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCMG.Monitoring.Util
{
    /// <summary>
    /// 邮件处理
    /// </summary>
    public class MailUtil
    {
        public void SendEMail(KeyValuePair<string, string>[] to,string subject,string htmlBody)
        {
            var client = new SmtpClient();
            try
            {
                var message = new MimeKit.MimeMessage();
                message.From.Add(new MailboxAddress(Configs.Config["email:from"], Configs.Config["email:address"]));
                if (to != null)
                    foreach (KeyValuePair<string, string> i in to)
                        message.To.Add(new MailboxAddress(i.Key, string.IsNullOrEmpty(i.Value) ? i.Key : i.Value));
                message.Subject = subject;

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(Configs.Config["email:smtp"], int.Parse(Configs.Config["email:port"]), false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Configs.Config["email:user"], Configs.Config["email:pwd"]);

                var body = new BodyBuilder();
                body.HtmlBody = htmlBody;
                message.Body = body.ToMessageBody();

                client.Send(message);
            }
            catch(Exception ex)
            {
                LogUtil.WriteLog(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
            }
        }
    }
}

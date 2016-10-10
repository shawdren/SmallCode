using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SmallCode
{
    public class EmailParameter
    {
        public List<string> MailTo { get; set; }
        public List<string> CC { get; set; }
        public List<string> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string AttachmentPath { get; set; }
    }

    public class EmailHelper
    {
        public string SendMail(EmailParameter ep)
        {
            MailMessage mailmsg = new MailMessage();
            mailmsg.From = new MailAddress("admin@demo.com", "admin", Encoding.UTF8);

            if (!string.IsNullOrEmpty(ep.AttachmentPath))
            {
                Attachment attachMent = new Attachment(ep.AttachmentPath, MediaTypeNames.Application.Octet);
                mailmsg.Attachments.Add(attachMent);
            }
            foreach (var addr in ep.MailTo)
            {
                mailmsg.To.Add(addr);
            }
            foreach (var addr in ep.CC)
            {
                mailmsg.CC.Add(addr);
            }
            foreach (var addr in ep.Bcc)
            {
                mailmsg.Bcc.Add(addr);
            }
            mailmsg.Subject = ep.Subject;
            mailmsg.Body = ep.Body;
            mailmsg.SubjectEncoding = Encoding.UTF8;
            mailmsg.IsBodyHtml = true;
            mailmsg.BodyEncoding = Encoding.UTF8;
            mailmsg.IsBodyHtml = true;
            mailmsg.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(@"admin", @"111111");  
            client.Host = "mail.demo.com";

            try
            {
                client.Send(mailmsg);
                return "Email sent successfully!!";

            }
            catch (Exception ex)
            {
                return ep.MailTo + "-Failed: " + ex.Message.ToString();
            }
        }
    }
}

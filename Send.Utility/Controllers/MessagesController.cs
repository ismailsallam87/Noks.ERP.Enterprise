using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace Send.Utility.Controllers
{
    [Route("Message")]
    public class MessagesController : ApiController
    {
        [HttpGet]
        [Route("SendMail")]
        public dynamic SendMail(string from, string to, string message, string password)
        {
            try
            {
                var boddy = new StringBuilder();
                boddy.Append(message);
                string bodyFor = boddy.ToString();
                string subjectFor = "AlbumGame Invitation";
                string toFor = to;
                var fromAddress = new MailAddress(from, "");//("albumgameeg@gmail.com", "Album Game");
                var toAddress = new MailAddress("", "");//(toFor, "To Name");
                const string fromPassword = "";//"albumgameeg@1234";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var msg = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subjectFor,
                    Body = bodyFor
                })
                {
                    smtp.Send(msg);
                }
                return "message sent successfully";
            }
            catch
            {
                return "message not sent";
            }
        }
    }
}

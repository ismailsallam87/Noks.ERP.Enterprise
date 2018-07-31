using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace MeetingRoom.API.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("SendMail")]
        public dynamic SendMail(string to, string message)
        {
            try
            {
                //var boddy = new StringBuilder();
                //boddy.Append(message);
                //string bodyFor = boddy.ToString();
                string subjectFor = "AlbumGame Invitation";
                var fromAddress = new MailAddress("albumgameeg@gmail.com", "Album Game");
                var toAddress = new MailAddress(to, "To Name");
                const string fromPassword = "albumgameeg@1234";
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
                    Body = message
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

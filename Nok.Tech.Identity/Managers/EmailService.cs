using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Nok.Tech.Identity.Managers
{
    internal class EmailService: IIdentityMessageService
    {
        public EmailService()
        {
        }

        public Task SendAsync(IdentityMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
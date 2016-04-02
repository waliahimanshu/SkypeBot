using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Skype.Bots;
using Microsoft.Skype.Bots.Interfaces;

namespace SkypeBot.Controllers
{
    public class EchoBotController : BotController
    {
        public EchoBotController(IMessagingBotService messagingBotService) :
            base(messagingBotService, "MyChitterChatBot")
        {

        }

        [Route("v1/echo")]
        public override Task<HttpResponseMessage> ProcessMessagingEventAsync()
        {
            return base.ProcessMessagingEventAsync();
        }

    }
}
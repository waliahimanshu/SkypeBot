using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using Microsoft.Skype.Bots;
using Microsoft.Skype.Bots.Interfaces;

namespace SkypeBot
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            var container = new UnityContainer();
            config.DependencyResolver = new UnityDependencyResolver(container);

            RegisterTypes(container);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void RegisterTypes(UnityContainer container)
        {
            MessagingBotServiceSettings settings = MessagingBotServiceSettings.LoadFromCloudConfiguration();
            IMessagingBotService botService = new BotService(settings);
            container.RegisterInstance(botService, new ContainerControlledLifetimeManager());

            botService.OnPersonalChatMessageReceivedAsync +=
                async receivedEvent =>
                    await receivedEvent.Reply(receivedEvent.Content, true).ConfigureAwait(false);

            botService.OnContactAddedAsync +=
                async contactAdded =>
                    await contactAdded.Reply($"Hello {contactAdded.FromDisplayName}!").ConfigureAwait(false);
        }
    }
}


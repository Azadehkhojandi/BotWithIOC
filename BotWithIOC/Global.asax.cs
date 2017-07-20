using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using BotWithIOC.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace BotWithIOC
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public sealed class ChatBotModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                // register the top level dialog
                builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerDependency();

            }
        }

        protected void Application_Start()
        {
            this.RegisterBotDependencies();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        private void RegisterBotDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ReflectionSurrogateModule());

            builder.RegisterModule<ChatBotModule>();

           

            builder.Update(Conversation.Container);

            
        }
    }
}

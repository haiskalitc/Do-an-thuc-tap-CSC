using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(SourceMau.Startup))]
namespace SourceMau
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var unityHubActivator = new MvcHubActivator();

            GlobalHost.DependencyResolver.Register(
                typeof(IHubActivator),
                () => unityHubActivator);
            app.MapSignalR();
        }

        public class MvcHubActivator : IHubActivator
        {
            public IHub Create(HubDescriptor descriptor)
            {
                return (IHub)DependencyResolver.Current
                    .GetService(descriptor.HubType);
            }
        }
    }
}

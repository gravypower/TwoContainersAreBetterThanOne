using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Extensions;

namespace TwoContainersAreBetterThanOne.CompositionRoot
{
    public sealed class TwoContainersAreBetterThanOneControllerFactory : SitecoreControllerFactory
    {
        private static readonly Container Container;
        static TwoContainersAreBetterThanOneControllerFactory()
        {
            Container = Bootstrapper.Bootstrap();
        }

        public TwoContainersAreBetterThanOneControllerFactory(
            IControllerFactory innerFactory) : base(innerFactory)
        { 
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (controllerName.EqualsText(SitecoreControllerName))
                return CreateSitecoreController(requestContext, controllerName);
            
            var controllerType = GetControllerType(requestContext, controllerName);

            return Container.GetInstance(controllerType) as IController;
        }
    }
}
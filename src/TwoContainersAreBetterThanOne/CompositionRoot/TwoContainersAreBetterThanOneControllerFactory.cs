﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Configuration;
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
            try
            {
                if (controllerName.EqualsText(SitecoreControllerName))
                    return CreateSitecoreController(requestContext, controllerName);

                var controllerType = GetControllerType(requestContext, controllerName);

                try
                {
                    return Container.GetInstance(controllerType) as IController;
                }
                catch (ActivationException e)
                {
                    Log.Info($"SimpleInjector could not resolve type, {controllerType.Name}", e);
                }

                return ResolveController(controllerType);
            }
            catch (Exception e)
            {
                if (MvcSettings.DetailedErrorOnMissingController)
                    throw CreateControllerCreationException(controllerName, e);
                throw;
            }
        }
    }
}
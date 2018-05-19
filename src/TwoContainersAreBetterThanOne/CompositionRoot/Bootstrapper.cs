using System;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using TwoContainersAreBetterThanOne.Controllers;
using TwoContainersAreBetterThanOne.Usecases;

namespace TwoContainersAreBetterThanOne.CompositionRoot
{
    public static class Bootstrapper
    {
        private static Container _container;

        public static Container Bootstrap()
        {
            _container = new Container();

            _container.RegisterSingleton<ILoggedOnUserUsecase, SitecoreLoggedOnUserUsecase>();
            RegisterController<HelloController>();

#if DEBUG
            _container.Verify(VerificationOption.VerifyAndDiagnose);
#else
            _container.Verify();
#endif
            return _container;
        }

        private static void RegisterController<TController>()
        where TController : IController
        {
            var controllerType = typeof(TController);
            var lifestyle = _container.Options.LifestyleSelectionBehavior.SelectLifestyle(controllerType);
            var registration = lifestyle.CreateRegistration(controllerType, _container);

            // Microsoft.AspNetCore.Mvc.Controller implements IDisposable (which is a design flaw).
            // This will cause false positives in Simple Injector's diagnostic services, so we suppress
            // this warning in case the registered type doesn't override Dispose from Controller.
            registration.SuppressDiagnosticWarning(
                DiagnosticType.DisposableTransientComponent,
                "Derived type doesn't override Dispose, so it can be safely ignored.");

            _container.AddRegistration<IController>(registration);
        }
    }
}
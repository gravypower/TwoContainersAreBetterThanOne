using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace TwoContainersAreBetterThanOne.CompositionRoot
{
    public static class Bootstrapper
    {
        private static Container _container;

        private static readonly Assembly[] BootstrapAssemblies = { typeof(IBootstrap).Assembly };

        public static Container Bootstrap()
        {
            _container = new Container();

            foreach (var bootstrapType in GetBootstrapTypes())
            {
                var bootstrap = (IBootstrap)Activator.CreateInstance(bootstrapType);
                bootstrap.Bootstrap(_container);
            }

#if DEBUG
            _container.Verify(VerificationOption.VerifyAndDiagnose);
#else
            _container.Verify();
#endif

            return _container;
        }

        public static IEnumerable<Type> GetBootstrapTypes() =>
            from assembly in BootstrapAssemblies
            from type in assembly.GetExportedTypes()
            where typeof(IBootstrap).IsAssignableFrom(type) && !type.IsAbstract
            select type;
    }
}
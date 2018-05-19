using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace TwoContainersAreBetterThanOne.CompositionRoot
{
    public interface IBootstrap
    {
        void Bootstrap(Container container);
    }
}

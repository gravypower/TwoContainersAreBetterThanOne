using Sitecore.Pipelines;

namespace TwoContainersAreBetterThanOne.CompositionRoot
{
    public class InitialiseControllerFactory
    {
        public virtual void Process(PipelineArgs args)
        {
            var controllerBuilder = System.Web.Mvc.ControllerBuilder.Current;
            var controllerFactory = new TwoContainersAreBetterThanOneControllerFactory(controllerBuilder.GetControllerFactory());
            controllerBuilder.SetControllerFactory(controllerFactory);
        }
    }
}
namespace TwoContainersAreBetterThanOne.Usecases
{
    public class SitecoreLoggedOnUserUsecase:ILoggedOnUserUsecase
    {
        public string Name => Sitecore.Context.User.Name;
    }
}
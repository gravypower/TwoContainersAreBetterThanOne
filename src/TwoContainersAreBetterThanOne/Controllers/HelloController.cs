using System.Web.Mvc;
using TwoContainersAreBetterThanOne.Adapters;
using TwoContainersAreBetterThanOne.Models;

namespace TwoContainersAreBetterThanOne.Controllers
{
    public class HelloController : Controller
    {
        private readonly ILoggedOnUser _loggedOnUser;

        public HelloController(ILoggedOnUser loggedOnUser)
        {
            _loggedOnUser = loggedOnUser;
        }

        public ActionResult Index()
        {
            return View(new User {Name = _loggedOnUser.Name});
        }
    }
}
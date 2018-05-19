using System.Web.Mvc;
using TwoContainersAreBetterThanOne.Models;
using TwoContainersAreBetterThanOne.Usecases;

namespace TwoContainersAreBetterThanOne.Controllers
{
    public class HelloController : Controller
    {
        private readonly ILoggedOnUserUsecase _loggedOnUserUsecase;

        public HelloController(ILoggedOnUserUsecase loggedOnUserUsecase)
        {
            _loggedOnUserUsecase = loggedOnUserUsecase;
        }

        public ActionResult Index()
        {
            return View(new User {Name = _loggedOnUserUsecase.Name});
        }
    }
}
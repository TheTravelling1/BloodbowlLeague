using System.Web.Mvc;

namespace BloodbowlLeague.Mvc.Controllers
{
    public class TeamsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}
using System.Linq;
using System.Web.Mvc;
using BloodbowlLeague.Logic;
using BloodbowlLeague.Mvc.Models;

namespace BloodbowlLeague.Mvc.Controllers
{
    public class TeamsController: Controller
    {
        private readonly IRaceRepository _raceRepository;

        public TeamsController( IRaceRepository raceRepository )
        {
            _raceRepository = raceRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var viewModel = new NewTeamViewModel {
                AvailableRaces = _raceRepository.GetAll().Select( r => r.Name ).ToArray()
            };

            return View( viewModel );
        }
    }
}
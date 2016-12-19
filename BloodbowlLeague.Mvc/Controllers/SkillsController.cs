using System.Web.Mvc;
using BloodbowlLeague.Logic;
using BloodbowlLeague.Mvc.Models;

namespace BloodbowlLeague.Mvc.Controllers
{
    public class SkillsController: Controller
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController( ISkillRepository skillRepository )
        {
            _skillRepository = skillRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View( _skillRepository.GetAll() );
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new NewSkillViewModel();
            return View( viewModel );
        }

        [HttpPost]
        public ActionResult Add( NewSkillViewModel toAdd )
        {
            var skill = new Skill( toAdd.Name, toAdd.Description );
            _skillRepository.Save( skill );
            return RedirectToAction( "Index" );
        }
    }
}
using ExamTask4.Business.Services.Abstracts;
using ExamTask4.Core.RepositoryAbstract;
using ExamTask4.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask4.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChefService _chefService;

        public HomeController(IChefService chefService)
        {
            _chefService = chefService;
        }
        public IActionResult Index()
        {
            var chefs = _chefService.GetAllChefs();
            HomeVM vm = new()
            {
                Chefs = chefs
            };
            return View(vm);
        }
    }
}

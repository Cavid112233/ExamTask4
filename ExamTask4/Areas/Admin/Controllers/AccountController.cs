using Microsoft.AspNetCore.Mvc;

namespace ExamTask4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

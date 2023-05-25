using Microsoft.AspNetCore.Mvc;

namespace Bilet_1.Areas.Admin.Controllers
{
 
    public class DashBoardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

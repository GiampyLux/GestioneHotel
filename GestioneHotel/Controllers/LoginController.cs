using Microsoft.AspNetCore.Mvc;

namespace GestioneHotel.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

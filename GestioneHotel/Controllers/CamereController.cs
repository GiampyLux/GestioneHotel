using Microsoft.AspNetCore.Mvc;

namespace GestioneHotel.Controllers
{
    public class CamereController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

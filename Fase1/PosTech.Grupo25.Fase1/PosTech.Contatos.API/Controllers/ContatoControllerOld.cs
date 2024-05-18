using Microsoft.AspNetCore.Mvc;

namespace PosTech.Contatos.API.Controllers
{
    public class ContatoControllerOld : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

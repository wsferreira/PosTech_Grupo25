using Microsoft.AspNetCore.Mvc;

namespace PosTech.Contatos.API.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

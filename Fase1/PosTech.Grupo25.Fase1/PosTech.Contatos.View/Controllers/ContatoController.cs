using Microsoft.AspNetCore.Mvc;

namespace PosTech.Contatos.View.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

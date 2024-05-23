using Microsoft.AspNetCore.Mvc;
using PosTech.Contatos.API.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace PosTech.Contatos.View.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IConfiguration _configuration;

        public ContatoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: Contato
        public IActionResult Index()
        {
            List<Contato> list = ObterTodos();
            return View(list);
        }

        public IActionResult AdicionaContato()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Contato _contato)
        {
            try
            {
                string? applicationUrl = _configuration.GetValue<string>("Authentication:ApplicationUrl");
                HttpClient cliente = new HttpClient();
                var requestBody = JsonSerializer.Serialize<Contato>(_contato);
                HttpContent content = new StringContent(requestBody);
                using (var response = cliente.PostAsync(applicationUrl + "Cadastrar", content).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel adicionar - Status: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }  
            return View();
        }

        public List<Contato> ObterTodos()
        {
            List<Contato> list = new();
            try
            {
                string? applicationUrl = _configuration.GetValue<string>("Authentication:ApplicationUrl");
                HttpClient cliente = new HttpClient();
                using (var response = cliente.GetAsync(applicationUrl + "ObterTodos").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        var responseObj = JsonSerializer.Deserialize<List<Contato>>(responseBody);
                        list = responseObj.ToList();
                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel obter a lista (ObterTodos) - Status: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }
    }
}

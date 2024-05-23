using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using PosTech.Contatos.API.Interfaces;
using PosTech.Contatos.API.Models;
using PosTech.Contatos.View.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PosTech.Contatos.View.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
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
                    if(response.IsSuccessStatusCode)
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


        public  IActionResult Index()
        {
            List<Contato> list = ObterTodos();
            return View(list);
        }

        [Route("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

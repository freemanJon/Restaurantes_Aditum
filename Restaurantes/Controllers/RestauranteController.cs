using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurantes.Models;
using Restaurantes.Services;
using Restaurantes.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Aditum.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly RestauranteService _resSVC;

        public RestauranteController(RestauranteService restauranteService)
        {
            _resSVC = restauranteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult<IList<Restaurante>> List() => View(_resSVC.GetAll());

        public ActionResult CreateRestaurantesInDatabase()
        {
            List<Restaurante> restaurantes = _resSVC.GetAll();
            if (restaurantes == null)
            {
                LeitorCSV leitor = new LeitorCSV();
                restaurantes = leitor.ConverterCSV("./restaurant-hours.csv");
                foreach (var restaurante in restaurantes)
                {
                    _resSVC.Create(restaurante);
                }
            }

            return RedirectToAction("List");
        }

        public ActionResult FilterByTime(string time)
        {
            List<Restaurante> restaurantes = _resSVC.GetByFilterTime(time);
            return View("List", restaurantes);


        }

    }
}
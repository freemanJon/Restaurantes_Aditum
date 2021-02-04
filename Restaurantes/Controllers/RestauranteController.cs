using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurantes.Models;
using Restaurantes.Services;
using Restaurantes.Utils;
using Microsoft.AspNetCore.Mvc;
using Restaurantes.Interface;
using System.Runtime.InteropServices;
using System.IO;

namespace Aditum.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public RestauranteController(IRestauranteRepository _restauranteRepository)
        {
            this._restauranteRepository = _restauranteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Restaurante> restaurantes = _restauranteRepository.GetAll();
            if (restaurantes == null) 
            {
                return View("Index");
            }
            else if(restaurantes.Count == 0)
            {
                return View("Index");
            }
            else
            {
                return View("List", restaurantes);
            }
            
        }
        
        public ActionResult CreateRestaurantesInDatabase()
        {
            LeitorCSV leitor = new LeitorCSV();
            List<Restaurante> restaurantes = leitor.ConverterCSV(Path.GetFullPath("restaurant-hours.csv"));
            //Laço onde adicionará o restaurante no banco de dados caso ele ainda não seja encontrado, dessa forma evita dados repetidos e caso o csv seja atualizado pegará o dado novo.
            foreach (var restaurante in restaurantes)
            {
                if (_restauranteRepository.GetByName(restaurante.Nome) == null)
                {
                    if (!(String.IsNullOrEmpty(restaurante.Nome) || String.IsNullOrEmpty(restaurante.Time)))
                    {
                        _restauranteRepository.Create(restaurante);
                    }
                }
            }

            return RedirectToAction("List");
        }

        public ActionResult FilterByTime(string time)
        {
            List<Restaurante> restaurantes = new List<Restaurante>();

            try
            {
                TimeSpan hour = TimeSpan.Parse(time);
                restaurantes = _restauranteRepository.GetByFilterTime(hour);
            }
            catch (OverflowException)
            {
                restaurantes = _restauranteRepository.GetAll();
            }
            catch (FormatException)
            {
                restaurantes = _restauranteRepository.GetAll();
            }

            return View("List", restaurantes);

        }

    }
}
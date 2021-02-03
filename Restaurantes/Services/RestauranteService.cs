using Restaurantes.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurantes.Repositories;

namespace Restaurantes.Services
{
    public class RestauranteService:IRestauranteRepository
    {
        private readonly IMongoCollection<Restaurante> _restaurantes;

        public RestauranteService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _restaurantes = database.GetCollection<Restaurante>("Restaurante");
        }

        public bool Create(Restaurante restaurante)
        {
            try
            {
                _restaurantes.InsertOne(restaurante);
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public List<Restaurante> GetAll()
        {
            List<Restaurante> restaurantes = _restaurantes.Find(x => true).ToList();
            return restaurantes;
        }

        public List<Restaurante> GetByFilterTime(string time)
        {
            List<Restaurante> AuxRestaurantes = GetAll();
            List<Restaurante> restaurantes = new List<Restaurante>();
            foreach(var restaurante in AuxRestaurantes)
            {
                if((TimeSpan.Parse(restaurante.OpenTime) <= TimeSpan.Parse(time) && TimeSpan.Parse(restaurante.CloseTime) >= TimeSpan.Parse(time))){
                    restaurantes.Add(restaurante);
                }
            }
            return restaurantes;
        }
    }
}

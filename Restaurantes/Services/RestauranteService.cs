using Restaurantes.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurantes.Interface;

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

        public Restaurante GetByName(string name)
        {
            Restaurante restaurante = _restaurantes.Find(x => x.Nome == name).FirstOrDefault();
            return restaurante;
        }

        public List<Restaurante> GetAll()
        {
            List<Restaurante> restaurantes = _restaurantes.Find(x => true).ToList();
            return restaurantes;
        }

        public List<Restaurante> GetByFilterTime(TimeSpan time)
        {
            List<Restaurante> AuxRestaurantes = GetAll();
            List<Restaurante> restaurantes = new List<Restaurante>();
            foreach(var restaurante in AuxRestaurantes)
            {
                if((TimeSpan.Parse(restaurante.OpenTime) <= time && TimeSpan.Parse(restaurante.CloseTime) >= time)){
                    restaurantes.Add(restaurante);
                }
            }
            return restaurantes;
        }
    }
}

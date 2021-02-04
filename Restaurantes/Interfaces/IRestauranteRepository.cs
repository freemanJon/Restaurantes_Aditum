using Restaurantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantes.Interface
{
    public interface IRestauranteRepository
    {
        bool Create(Restaurante restaurante);
        List<Restaurante> GetAll();
        List<Restaurante> GetByFilterTime(TimeSpan time);
        Restaurante GetByName(string name);
    }
}

using Restaurantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantes.Repositories
{
    public interface IRestauranteRepository
    {
        bool Create(Restaurante restaurante);
        List<Restaurante> GetAll();
        List<Restaurante> GetByFilterTime(string time);
    }
}

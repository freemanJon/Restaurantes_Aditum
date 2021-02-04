using Restaurantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantes.Interfaces
{
    public interface ILeitorCSV
    {
        List<Restaurante> ConverterCSV(string path);
    }
}

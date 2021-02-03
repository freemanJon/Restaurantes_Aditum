using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantes.Models
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class DataBaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get ; set ;}
        public string DatabaseName { get; set ; }
    }
}

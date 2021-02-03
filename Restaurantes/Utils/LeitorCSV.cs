using Restaurantes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantes.Utils
{
    public class LeitorCSV
    {
        //LER ARQUIVO CSV E TRANSFORMAR EM OBJETO RESTAURANTE
        //Complexidade O(n)
        public List<Restaurante> ConverterCSV(string path)
        {
            List<Restaurante> restaurantes = File.ReadAllLines(path)
                        .Select(x => x.Split(','))
                        .Select(y => new Restaurante()
                        {
                            //y[0] coluna do csv com os nomes dos restaurantes
                            Nome = y[0],
                            //y[1] coluna do csv com os horarios que os restaurantes abrem e fecham
                            Time = y[1]
                        }).ToList();

            //Eliminar a primeira linha que são os cabeçalhos do CSV
            restaurantes.RemoveAt(0);
            //Foreach para pegar o time e converter open e close para timespan, tentei executar dentro do select o comando abaixo porém não funcionou, gerando um pouco mais de complexidade
            foreach (var restaurante in restaurantes)
            {
                restaurante.OpenTime = restaurante.Time.Split('-')[0];
                restaurante.CloseTime = restaurante.Time.Split('-')[1];
            }
            return restaurantes;
        }
    }
}

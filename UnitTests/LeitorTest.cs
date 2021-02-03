using Restaurantes.Models;
using Restaurantes.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class LeitorTest
    {
        [Fact]
        public void ValidacaoConverterCSVToListRestaurantes()
        {
            LeitorCSV leitor = new LeitorCSV();
            //Trocar para o path onde se encontra o arquivo CSV Test
            List<Restaurante> restaurantes = leitor.ConverterCSV(@"C:\Users\Jonatan\source\repos\Restaurantes\UnitTests\restaurant-hours-test.csv");
            //51 é o numero de registro no excel, logo se a quantidade de restaurantes na lista for 51, a conversão ocorreu corretamente.
            Assert.Equal(51, restaurantes.Count);
        }
    }
}

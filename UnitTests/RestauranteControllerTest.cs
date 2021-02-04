using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Restaurantes.Models;
using Restaurantes.Interface;
using Restaurantes.Services;
using System;
using Xunit;
using Aditum.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restaurantes.Interfaces;

namespace UnitTests
{
    public class RestauranteTest
    {

        //Teste feito para verificar se ao mandar a filtragem corretamente é carregada a view List novamente com a listagem filtrada
        [Fact]
        public void FilterByTime_OKResult()
        {
            List<Restaurante> restaurantes = new List<Restaurante>();
            string time = "09:00";
            TimeSpan hour = TimeSpan.Parse("09:00");
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetByFilterTime(hour)).Returns(restaurantes);
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.FilterByTime(time);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("List", viewResult.ViewName);
            Assert.Equal(restaurantes, viewResult.Model);
        }

        //Teste feito para verificar se ao mandar a filtragem(hora:minuto) com dados invalidos, apenas retornar uma lista com todos restaurantes 
        [Fact]
        public void FilterByTime_Error_InvalidTime()
        {
            List<Restaurante> restaurantes = new List<Restaurante>();
            string hour = "25:00";
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetAll()).Returns(restaurantes);
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.FilterByTime(hour);
            mockRestaurante.Verify(x => x.GetAll(), Times.Once);
        }

        //Teste feito para verificar se ao mandar a filtragem(hora:minuto) em branco, apenas retornar uma lista com todos restaurantes 
        [Fact]
        public void FilterByTime_Error_Empty()
        {
            List<Restaurante> restaurantes = new List<Restaurante>();
            string hour = "";
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetAll()).Returns(restaurantes);
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.FilterByTime(hour);
            mockRestaurante.Verify(x => x.GetAll(), Times.Once);
        }

        //Teste está OK, porém o arquivo não consegue ser encontrado, pois procura em um diretório diferente quando inicia o teste
        //Caso queira ver a funcionalidade do teste add o arquivo 'restaurant-hours.csv' no caminho ~\Restaurantes\UnitTests\bin\Debug\netcoreapp3.1 e descomentar
        //Teste feito para verificar se ao mandar um objeto corretamente, ele é criado
        //[Fact]
        //public void CreateRestauranteInDataBase_OKResult()
        //{
        //    Restaurante restauranteFake = new Restaurante() { Nome = "Mc Donalds", OpenTime = "09:00", CloseTime = "21:00", Time = "09:00-21:00" };
        //    var mockRestaurante = new Mock<IRestauranteRepository>();
        //    mockRestaurante.Setup(x => x.GetByName(It.IsAny<string>())).Returns(default(Restaurante));
        //    mockRestaurante.Setup(x => x.Create(It.IsAny<Restaurante>())).Returns(true);
        //    RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
        //    var result = restauranteController.CreateRestaurantesInDatabase();
        //    mockRestaurante.Verify(x => x.Create(It.IsAny<Restaurante>()), Times.AtLeast(1));

        //}


        //Tentativa de teste, porém estou com dificuldade em definir uma lista de restaurantes para executar o teste.
        //Teste feito para ver se ao mandar um objeto com algum campo em branco não adiciona-lo
        //[Fact]
        //public void CreateRestauranteInDataBase_Error_AnyFieldEmpty()
        //{
        //    List<Restaurante> restaurantesFake = new List<Restaurante>();
        //    Restaurante restauranteFake = new Restaurante() { Nome = "", OpenTime = "", CloseTime = "", Time = "" };
        //    restaurantesFake.Add(restauranteFake)
        //    var mockRestaurante = new Mock<IRestauranteRepository>();
        //    var mockLeitorCSV = new Mock<ILeitorCSV>();
        //    mockLeitorCSV.Setup(x => x.ConverterCSV(It.IsAny<string>())).Returns(restaurantesFake);
        //    mockRestaurante.Setup(x => x.GetByName(It.IsAny<string>())).Returns(restauranteFake);
        //    mockRestaurante.Setup(x => x.Create(It.IsAny<Restaurante>())).Returns(true);
        //    RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
        //    var result = restauranteController.CreateRestaurantesInDatabase();
        //    mockRestaurante.Verify(x => x.Create(It.IsAny<Restaurante>()), Times.Never);

        //}

        //Teste feito para ver se ao mandar uma lista com elementos está sendo direcionada para view List
        [Fact]
       
        public void List_OKResult()
        {
            List<Restaurante> restaurantesFake = new List<Restaurante>();
            Restaurante restauranteFake = new Restaurante() { Nome = "", OpenTime = "", CloseTime = "", Time = "" };
            restaurantesFake.Add(restauranteFake);
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetAll()).Returns(restaurantesFake);
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("List", viewResult.ViewName);
            Assert.Equal(restaurantesFake, viewResult.Model);

        }

        //Teste feito para ver se ao mandar uma lista vazia continua na mesma view, no caso Index
        [Fact]
        
        public void List_NullList()
        {
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetAll()).Returns(default(List<Restaurante>));
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);

        }

        //Teste feito para ver se ao mandar uma lista vazia continua na mesma view, no caso Index
        [Fact]
       
        public void List_EmptyList()
        {
            List<Restaurante> restaurantesFake = new List<Restaurante>();
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetAll()).Returns(restaurantesFake);
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);

        }
    }
}

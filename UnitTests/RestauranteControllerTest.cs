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

        [Fact]
        public void CreateRestauranteInDataBase_OKResult()
        {
            Restaurante restauranteFake = new Restaurante() { Nome = "Mc Donalds", OpenTime = "09:00", CloseTime = "21:00", Time = "09:00-21:00" };
            var mockRestaurante = new Mock<IRestauranteRepository>();
            mockRestaurante.Setup(x => x.GetByName(It.IsAny<string>())).Returns(default(Restaurante));
            mockRestaurante.Setup(x => x.Create(It.IsAny<Restaurante>())).Returns(true);
            RestauranteController restauranteController = new RestauranteController(mockRestaurante.Object);
            var result = restauranteController.CreateRestaurantesInDatabase();
            mockRestaurante.Verify(x => x.Create(It.IsAny<Restaurante>()), Times.AtLeast(1));

        }


        //Tentativa de teste, porém estou com dificuldade em definir uma lista de restaurantes para executar o teste.
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

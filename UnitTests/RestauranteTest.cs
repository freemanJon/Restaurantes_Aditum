using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Restaurantes.Models;
using Restaurantes.Repositories;
using Restaurantes.Services;
using System;
using Xunit;

namespace UnitTests
{
    public class RestauranteTest
    {
        private Mock<IRestauranteRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private RestauranteService _service;
        private DataBaseSettings settings = new DataBaseSettings() { DatabaseName= "Aditum_Restaurantes", ConnectionString= "mongodb+srv://webapp:1234abcd@cluster0.4uehh.mongodb.net/Aditum_Restaurantes?retryWrites=true&w=majority" };

        public  RestauranteTest()
        {
            _mockRepository = new Mock<IRestauranteRepository>();
            _modelState = new ModelStateDictionary();
            _service = new RestauranteService(settings);
        }

        [Fact]
        public void ValidateCreate()
        {
            Restaurante restaurante = new Restaurante() { Id = "214155151", Nome = "Burguer King", OpenTime = "08:00", CloseTime = "21:00", Time = "08:00-21:00" };
            bool created = _service.Create(restaurante);
            Assert.True(created);
        }
    }
}

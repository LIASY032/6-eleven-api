using System;
using System.Net;
using System.Net.Http;
using AutoMapper;
using FluentAssertions;
using Item.API.Controllers;
using Item.API.DTO;
using Item.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Item.API.Test.Controllers
{
    public class TestItemController
    {
        //private Mock< ILogger<ItemController>> _logger;
        //private Mock<IProductRepository >_repository;
        //private Mock<IMapper >_mapper;
        private readonly ITestOutputHelper output;
        //private readonly WebApplicationFactory<Program> _factory;
        public HttpClient _client { get; }
        public TestItemController(ITestOutputHelper output) {
            var webAppFactory = new WebApplicationFactory<Program>();
            _client = webAppFactory.CreateDefaultClient();
            this.output = output;

        }

        [Fact(DisplayName = "GET /item returns http status code 200")]
		public async void Get_OnSuccess_GetItem(){
            var response= await _client.GetAsync("/item");
            var result = await response.Content.ReadAsStreamAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact(DisplayName = "GET /item returns is application/json")]
        public async void Get_IsApplicationJSON_GetItem()
        {

            var response= await _client.GetAsync("/item");
            output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }
        [Fact(DisplayName = "GET /item returns is an array")]
        public async void Get_IsArray_GetItem()
        {
            var response = await _client.GetAsync("/item");
            var content = new Newtonsoft.Json.JsonSerializer().Deserialize<ProductDTO>(new JsonTextReader(new StreamReader( await response.Content.ReadAsStringAsync())));
            Assert.True(content.GetType().IsArray);

            //_repository.Setup(repo => repo.GetProducts());
            //var item = new ItemController(_mapper.Object, _repository.Object, _logger.Object);
            //var response = await item.GetItemAsync();
            //Assert.True(response.Value.GetType().IsArray);


        }
    }
}


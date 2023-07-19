using System;
using System.Net;
using AutoMapper;
using FluentAssertions;
using Item.API.Controllers;
using Item.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

namespace Item.API.Test.Controllers
{
    public class TestItemController
    {
        private Mock< ILogger<ItemController>> _logger;
        private Mock<IProductRepository >_repository;
        private Mock<IMapper >_mapper;
        private readonly ITestOutputHelper output;
        public TestItemController(ITestOutputHelper output) {
			_repository = new Mock<IProductRepository>();
			_logger = new Mock<ILogger<ItemController>>();
			_mapper = new Mock<IMapper>();
            this.output = output;

        }

        [Fact(DisplayName = "GET /item returns http status code 200")]
		public async void Get_OnSuccess_GetItem(){
			var item = new ItemController(_mapper.Object,_repository.Object,_logger .Object);
            var response= await item.GetItemAsync();
            var result = response.Result as OkObjectResult;
            result?.StatusCode.Should().Be(200);
        }
	}
}


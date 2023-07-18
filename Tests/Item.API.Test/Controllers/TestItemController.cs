using System;
using AutoMapper;
using FluentAssertions;
using Item.API.Controllers;
using Item.API.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Item.API.Test.Controllers
{
    public class TestItemController
    {
        private Mock< ILogger<ItemController>> _logger;
        private Mock<IProductRepository >_repository;
        private Mock<IMapper >_mapper;

        public void Startup() {
			_repository = new Mock<IProductRepository>();
			_logger = new Mock<ILogger<ItemController>>();
			_mapper = new Mock<IMapper>();
	
	}

        [Fact]
		public void Get_OnSuccess_GetItem(){
			var item = new ItemController(_mapper.Object,_repository.Object,_logger .Object);
			var result = item.GetItemAsync();
			result.Status.Should().Be((TaskStatus)200);
	}
	}
}


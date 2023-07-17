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
        [TestInitialize]
        public void SetUp()
        {
        }

        [Fact]
		public void Get_OnSuccess_GetItem(){
			var item = new ItemController(_mapper,_repository,_logger );
			var result = item.GetItemAsync();
			result.Status.Should().Be((TaskStatus)200);
	}
	}
}


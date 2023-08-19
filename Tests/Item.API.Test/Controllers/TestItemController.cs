using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Common.Extensions;
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
    [TestCaseOrderer(
    ordererTypeName: "Common.Extensions.PriorityOrderer",
    ordererAssemblyName: "Common")]
    public class TestItemController
    {
        //private Mock< ILogger<ItemController>> _logger;
        //private Mock<IProductRepository >_repository;
        //private Mock<IMapper >_mapper;
        private readonly ITestOutputHelper output;
        //private readonly WebApplicationFactory<Program> _factory;
        public HttpClient _client { get; }
        public string itemId { get; set; }
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
            //output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }
        [Fact(DisplayName = "GET /item returns is an array")]
        public async void Get_IsArray_GetItem()
        {
            var response = await _client.GetAsync("/item");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var content= System.Text.Json.JsonSerializer.Deserialize<List<ProductDTO>>(json, serializerOptions);
       
            Assert.True(typeof(IEnumerable).IsAssignableFrom(content.GetType())|| typeof(ICollection).IsAssignableFrom(content.GetType()));


        }

        [Fact(DisplayName = "GET /item/{id} returns an item")]
        public async void Get_Return_A_Item()
        {
            var response = await _client.GetAsync("/item");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var content = System.Text.Json.JsonSerializer.Deserialize<List<ProductDTO>>(json, serializerOptions);
            response = await _client.GetAsync($"/item/{content[0].Id}");
            response.EnsureSuccessStatusCode();
            json = await response.Content.ReadAsStringAsync();
         
            var item = System.Text.Json.JsonSerializer.Deserialize<ProductDTO>(json, serializerOptions);

            item.Title.Should().Be(content[0].Title);

        }


        [Fact(DisplayName = "POST /item Add an Item"), TestPriority(1)]
        public async void POST_Add_Item()
        {
            var newProduct = new ProductDTO() { Info = "Two hundred kilograms without switching shoulder!", Price=0, Title="BAODI", CollectionType="daily" };
            var response = await _client.PostAsJsonAsync("/item", newProduct);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var content = System.Text.Json.JsonSerializer.Deserialize<ProductDTO>(json, serializerOptions);

            this.itemId = content.Id;
            content.Title.Should().Be(newProduct.Title);
            content.Info.Should().Be(newProduct.Info);

        }


        [Fact(DisplayName = "PUT /item/{id} Update an Item"), TestPriority(2)]
        public async void PUT_Update_Item()
        {
            var newProduct = new ProductDTO() { Info = "Cary two hundred kilograms without switching shoulder!", Price = 0, Title = "WEINIXIONG", CollectionType = "daily" };
            var response = await _client.PutAsJsonAsync($"/item/{itemId}", newProduct);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var content = System.Text.Json.JsonSerializer.Deserialize<ProductDTO>(json, serializerOptions);


            content.Title.Should().Be(newProduct.Title);
            content.Info.Should().Be(newProduct.Info);

        }

        [Fact(DisplayName = "DELETE /item/{id} Delete an Item"), TestPriority(3)]
        public async void DELETE_Delete_Item()
        {
            var newProduct = new ProductDTO() { Info = "Cary two hundred kilograms without switching shoulder!", Price = 0, Title = "WEINIXIONG", CollectionType = "daily" };
            var response = await _client.DeleteAsync($"/item/{itemId}" );
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var content = System.Text.Json.JsonSerializer.Deserialize<ProductDTO>(json, serializerOptions);


            content.Title.Should().Be(newProduct.Title);
            content.Info.Should().Be(newProduct.Info);

        }


    }
}


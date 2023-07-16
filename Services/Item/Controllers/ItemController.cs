﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Item.API.DTO;
using Item.API.Entities;
using Item.API.Repositories;
using Item.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Item.API.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class ItemController: ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ItemController(IMapper mapper, IProductRepository repository, ILogger<WeatherForecastController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet( Name = "Get Items")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetItemAsync()
        {
            var products = await _repository.GetProducts();
            return Ok(_mapper.Map<List<ProductDTO>>(products));
        }

        [HttpGet("{id::length(24)}",Name = "Get Item by id")]
        public async Task<ActionResult<ProductDTO>> GetItemByIdAsync(string id)
        {
            var products = await _repository.GetProduct(id);
            return Ok(_mapper.Map<ProductDTO>(products));
        }

        [HttpPut("{id::length(24)}", Name = "Update Item by id")]
        public async Task<ActionResult<ProductDTO>> UpdateItemByIdAsync(string id, [FromBody] ProductDTO product)
        {
            //var products = await _repository.UpdateProduct(product);
            //return Ok(_mapper.Map<ProductDTO>(products));
            throw new NotImplementedException();
        }

        [HttpPost(Name = "Add the Item")]
        public async Task<ActionResult<ProductDTO>> CreateItem([FromBody] ProductDTO product) {
            var item= _mapper.Map<Product>(product);

            await _repository.CreateProduct(item);

            return Ok( item );
	}
        [HttpDelete("{id::length(24)}", Name ="Delete Item")]
        public async Task<ActionResult<ProductDTO >> DeleteItem(String id) {
            try { 
	    
            var item = await _repository.DeleteProduct(id);
                return Ok(_mapper.Map < ProductDTO > (item));
            }
            catch (Exception e) { 
	    
            return NotFound(id);
	    }
          
	}



    }
}

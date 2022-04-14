using Klir.TechChallenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Domain.Interfaces;
using Klir.TechChallenge.Domain.enumerators;

namespace Klir.TechChallenge.Web.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBaseService<Product> _baseProductService;

        public ProductController(IBaseService<Product> baseProductService, ILogger<ProductController> logger)
        {
            _baseProductService = baseProductService;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Execute(() => _baseProductService.Get<Product>());
        }

        [HttpGet("/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return id == 0 ? 
                Execute(() => _baseProductService.Get<Product>()) 
              : Execute(() => _baseProductService.GetById<Product>(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[ProductController] " + ex.Message);
                return BadRequest(ex);
            }
        }
    }
}

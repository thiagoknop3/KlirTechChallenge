using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.enumerators;
using Klir.TechChallenge.Domain.Interfaces;
using Klir.TechChallenge.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlirTechChallenge.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Klir.TechChallenge.Web.Api.Controllers.ControllerBase
    {

        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartService _service;

        public ShoppingCartController(IShoppingCartService service,
            ILogger<ShoppingCartController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<ShoppingCart> Get()
        {
            try
            {
                var result = _service.Get<ShoppingCart>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[ItemController] " + ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPut("AddItem")]
        public ActionResult<ShoppingCart> AddItem([FromBody] ItemCommand command)
        {
            try
            {
                var result = _service.AddItem(command.ProductId, command.Quantity);
                if (result.Success)
                    return Ok(result.Data);
                return MapToActionResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[ShoppingCartController] " + ex.Message);
                return Problem("Error while adding item", statusCode: 500);
            }
        }

        [HttpPut("RemoveItem")]
        public ActionResult<ShoppingCart> RemoveItem([FromBody] ItemCommand command)
        {
            try
            {
                var result = _service.RemoveItem(command.ProductId, command.Quantity);
                return MapToActionResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[ShoppingCartController] " + ex.Message);
                return Problem("Error while removing item", statusCode: 500);
            }
        }


    }
}

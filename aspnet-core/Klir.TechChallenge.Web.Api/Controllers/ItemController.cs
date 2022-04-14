using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Klir.TechChallenge.Web.Api.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IBaseService<Item> _baseItemService;

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseItemService.Get<Product>());
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
                _logger.LogError("[ItemController] " + ex.Message);
                return BadRequest(ex);
            }
        }
    }
}

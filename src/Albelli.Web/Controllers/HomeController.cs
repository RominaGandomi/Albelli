﻿using Albelli.Business.Models.Dto;
using Albelli.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Albelli.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderApiService _orderApiService;

        public HomeController(ILogger<HomeController> logger, IOrderApiService orderApiService)
        {
            _logger = logger;
            _orderApiService = orderApiService;
        }

        [HttpGet("isAlive")]
        public IActionResult IsAlive()
        {

            return Ok("I am alive");
        }

        [HttpGet("getOrder")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                var order = await _orderApiService.GetOrder(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation failed in GetOrder method");
                return Ok("Operation failed.");
            }
            
        }

        [HttpPost("submitOrder")]
        public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderInput model)
        {
            try
            {
                if (!ModelState.IsValid) return NoContent();
                var data = await _orderApiService.SaveOrder(model);
                return Ok(new { message = data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation failed in SubmitOrder method");
                return Ok("Operation failed.");
            }
        }
    }
}

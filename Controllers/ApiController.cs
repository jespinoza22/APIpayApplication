using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APIpayApplication.DbContext;
using Microsoft.EntityFrameworkCore;
using APIpayApplication.Repository;
using APIpayApplication.Models;

namespace APIpayApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ICrudRepository<Income>  _incomeRepository;

        public ApiController(ICrudRepository<Income> productRepository)
        {
            _incomeRepository = productRepository;
        }

        [HttpGet("public")]
        public IActionResult Public()
        {
            var incomes = _incomeRepository.getAll();
            return new OkObjectResult(incomes);
            //return Ok(new
            //{
            //    Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            //});
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            var incomes = _incomeRepository.getAll();
            return new OkObjectResult(incomes);
            //return Ok(new
            //{
            //    Message = "Hello the application works perfect"
            //});
        }
    }
}

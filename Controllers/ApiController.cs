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

namespace APIpayApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ICrudRepository<IncomeRepository> _incomeRepository;

        public ApiController(ICrudRepository<IncomeRepository> incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Ok(new
            {
                Message = "Hello the application works perfect"
            });
        }
    }
}

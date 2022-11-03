using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.CharecterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharecterController : ControllerBase
    {
        private readonly ICharecterService charecterService;
        public CharecterController(ICharecterService charecterService)
        {
            this.charecterService = charecterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Charecter>>> GetSingle(int id)
        {
            return Ok( await this.charecterService.GetCharecterById(id));
        }

        // [HttpGet]
        // [Route("GetAll") ]
        // Or  combine both into one. 
       [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Charecter>>>> Get()
        {
            return Ok( await this.charecterService.GetAllCharecters());
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Charecter>>>> AddCharecter(Charecter newCharecter)
        {
            return Ok(await this.charecterService.AddCharecter(newCharecter));
        }
        
    }
}
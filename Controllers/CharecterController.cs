using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charecter;
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
        public async Task<ActionResult<ServiceResponse<GetCharecterDto>>> GetSingle(int id)
        {
            return Ok( await this.charecterService.GetCharecterById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> Delete(int id)
        {
           var response = await this.charecterService.DeleteCharecter(id);
            if(response.Data==null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }
        // [HttpGet]
        // [Route("GetAll") ]
        // Or  combine both into one. 
       [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> Get()
        {
            return Ok( await this.charecterService.GetAllCharecters());
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> AddCharecter(AddCharecterDto newCharecter)
        {
            return Ok(await this.charecterService.AddCharecter(newCharecter));
        }

         [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> UpdateCharecter(UpdateCharecterDto updatedCharecter)
        {
            var response = await this.charecterService.UpdateCharecter(updatedCharecter);
            if(response.Data==null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }
        
    }
}
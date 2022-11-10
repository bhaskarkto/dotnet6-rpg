using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charecter;
using dotnet_rpg.Services.CharecterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharecterController : ControllerBase
    {
        private readonly ICharecterService _charecterService;
        public CharecterController(ICharecterService charecterService)
        {
            _charecterService = charecterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharecterDto>>> GetSingle(int id)
        {
            return Ok( await _charecterService.GetCharecterById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> Delete(int id)
        {
           var response = await _charecterService.DeleteCharecter(id);
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
            return Ok( await _charecterService.GetAllCharecters());
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> AddCharecter(AddCharecterDto newCharecter)
        {
            return Ok(await _charecterService.AddCharecter(newCharecter));
        }

         [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharecterDto>>>> UpdateCharecter(UpdateCharecterDto updatedCharecter)
        {
            var response = await _charecterService.UpdateCharecter(updatedCharecter);
            if(response.Data==null)
            {
                return NotFound(response); 
            }
            return Ok(response);
        }

         [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharecterDto>>> AddCharecterSkill(AddCharecterSkillDto newCharecterSkill)
        {
            return Ok(await _charecterService.AddCharecterSkill(newCharecterSkill));     
        }
        
    }
}
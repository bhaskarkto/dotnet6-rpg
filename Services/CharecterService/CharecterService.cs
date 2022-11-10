using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Charecter;
using dotnet_rpg.Dtos.Skill;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        // Except delete charecter everything is done, delete charecter is the only one using the Charecters no we can remove it.  
        //private static List<Charecter> charecters = new List<Charecter>{
        //     new Charecter(), 
        //     new Charecter {Id = 1, Name = "Sam"}
        // }; 
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharecterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            //Added Comment; 
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier)); 

        public async Task<ServiceResponse<List<GetCharecterDto>>> AddCharecter(AddCharecterDto newCharecter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharecterDto>>(); 
            Charecter charecter = _mapper.Map<Charecter>(newCharecter); 
            charecter.User = await _context.Users.FirstOrDefaultAsync(u=> u.Id == GetUserId()); 

            _context.Charecters.Add(charecter);
            await _context.SaveChangesAsync(); 
            serviceResponse.Data = await _context.Charecters
                .Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharecterDto>(c))
                .ToListAsync(); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> DeleteCharecter(int id)
        {
            ServiceResponse<List<GetCharecterDto>> response = new ServiceResponse<List<GetCharecterDto>>(); 
            try
            {
                Charecter charecter = await _context.Charecters
                    .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if(charecter != null)
                {
                    _context.Charecters.Remove(charecter); 
                    await _context.SaveChangesAsync(); 
                    response.Data = _context.Charecters
                        .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharecterDto>(c)).ToList(); 
                }
                else 
                {
                    response.Success = false;
                    response.message = "Charecter Not found";
                }
            }
            catch(Exception ex)
            { 
                response.Success = false; 
                response.message= ex.Message; 
            }
            return response;  
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> GetAllCharecters() //We don't need anymore (int userId)
        {
            var response = new ServiceResponse<List<GetCharecterDto>>(); 
            var dbCharecters = await _context.Charecters                        
                                .Where(c => c.User.Id ==  GetUserId())   //replace userID with GetUserId()
                                .ToListAsync(); 
                                // instead of just  .ToListAsync(); which returns all, we use a where clause to return only charecters that belong to the user

            response.Data = dbCharecters.Select(c => _mapper.Map<GetCharecterDto>(c)).ToList(); 
            return response; 
        }

        public async Task<ServiceResponse<GetCharecterDto>> GetCharecterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharecterDto>(); 
            var dbCharecter = await _context.Charecters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == GetUserId()); 
            serviceResponse.Data = _mapper.Map<GetCharecterDto>(dbCharecter) ;  
            return serviceResponse; 
        }

        public async Task<ServiceResponse<GetCharecterDto>> UpdateCharecter(UpdateCharecterDto updatedCharecter)
        {
            ServiceResponse<GetCharecterDto> response = new ServiceResponse<GetCharecterDto>(); 
            try
            {
                var charecter = await  _context.Charecters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharecter.Id);

                if(charecter.User.Id == GetUserId())
                {
                    _mapper.Map(updatedCharecter, charecter); 
                    // charecter.Name = updatedCharecter.Name; 
                    // charecter.HitPoints = updatedCharecter.HitPoints; 
                    // charecter.Strength =updatedCharecter.Strength; 
                    // charecter.Defense = updatedCharecter.Defense; 
                    // charecter.Intelligence = updatedCharecter.Intelligence;
                    // charecter.Class = updatedCharecter.Class ; 
                    await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharecterDto>(charecter);
                }
                else{
                    response.Success = false; 
                    response.message= "Charecter not found"; 
                } 
            }
            catch(Exception ex)
            { 
                response.Success = false; 
                response.message= ex.Message; 
            }
            return response;  
        }

        public async Task<ServiceResponse<GetCharecterDto>> AddCharecterSkill(AddCharecterSkillDto newCharecterSkill)
        {
           var response = new ServiceResponse<GetCharecterDto>(); 
           try{
                var charecter = await _context.Charecters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharecterSkill.CharecterID && 
                    c.User.Id == GetUserId()); 

                if(charecter == null)
                {
                    response.Success = false;
                    response.message = "Charecter Not found";
                    return response; 
                }

                var skill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newCharecterSkill.SkillId );

                if(skill == null)
                {
                    response.Success = false;
                    response.message = "skill Not found";
                    return response; 
                }

                charecter.Skills.Add(skill); 
                await _context.SaveChangesAsync(); 
                response.Data = _mapper.Map<GetCharecterDto>(charecter); 
           }
           catch(Exception ex) 
           {
                response.Success = false; 
                response.message = ex.Message; 
           }
           return response; 
        }

        // public Task<ServiceResponse<GetCharecterDto>> UpdateCharecter(UpdateCharecterDto updatedCharecter)
        // {
        //     ServiceResponse<GetCharecterDto> response = new ServiceResponse<GetCharecterDto>(); 
        //     Charecter charecter = charecters.FirstOrDefault(c => c.Id == updatedCharecter.Id);
        //     charecter.Name = updatedCharecter.Name; 
        //     charecter.HitPoints = updatedCharecter.HitPoints; 
        //     charecter.Strength =updatedCharecter.Strength; 
        //     charecter.Defense = updatedCharecter.Defense; 
        //     charecter.Intelligence = updatedCharecter.Intelligence;
        //     charecter.Class = updatedCharecter.Class ; 
        //     response.Data = _mapper.Map<GetCharecterDto>(charecter); 
        //     return response;  
        // }

    }
}
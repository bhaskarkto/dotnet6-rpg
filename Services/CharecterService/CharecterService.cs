using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Charecter;
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

        public CharecterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> AddCharecter(AddCharecterDto newCharecter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharecterDto>>(); 
            Charecter charecter = _mapper.Map<Charecter>(newCharecter); 
            _context.Charecters.Add(charecter);
            await _context.SaveChangesAsync(); 
            serviceResponse.Data = await _context.Charecters
                .Select(c => _mapper.Map<GetCharecterDto>(c))
                .ToListAsync(); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> DeleteCharecter(int id)
        {
            ServiceResponse<List<GetCharecterDto>> response = new ServiceResponse<List<GetCharecterDto>>(); 
            try
            {
                Charecter charecter = await _context.Charecters.FirstAsync(c => c.Id == id);
                _context.Charecters.Remove(charecter); 
                await _context.SaveChangesAsync(); 
                response.Data = _context.Charecters.Select(c => _mapper.Map<GetCharecterDto>(c)).ToList(); 
              
            }
            catch(Exception ex)
            { 
                response.Success = false; 
                response.message= ex.Message; 
            }
            return response;  
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> GetAllCharecters(int userId)
        {
            var response = new ServiceResponse<List<GetCharecterDto>>(); 
            var dbCharecters = await _context.Charecters                        
                                .Where(c => c.User.Id == userId)
                                .ToListAsync(); 
                                // instead of just  .ToListAsync(); which returns all, we use a where clause to return only charecters that belong to the user

            response.Data = dbCharecters.Select(c => _mapper.Map<GetCharecterDto>(c)).ToList(); 
            return response; 
        }

        public async Task<ServiceResponse<GetCharecterDto>> GetCharecterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharecterDto>(); 
            var dbCharecter = await _context.Charecters.FirstOrDefaultAsync(c => c.Id == id); 
            serviceResponse.Data = _mapper.Map<GetCharecterDto>(dbCharecter) ;  
            return serviceResponse; 
        }

        public async Task<ServiceResponse<GetCharecterDto>> UpdateCharecter(UpdateCharecterDto updatedCharecter)
        {
            ServiceResponse<GetCharecterDto> response = new ServiceResponse<GetCharecterDto>(); 
            try
            {
                var charecter = await  _context.Charecters
                    .FirstOrDefaultAsync(c => c.Id == updatedCharecter.Id);

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
            catch(Exception ex)
            { 
                response.Success = false; 
                response.message= ex.Message; 
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
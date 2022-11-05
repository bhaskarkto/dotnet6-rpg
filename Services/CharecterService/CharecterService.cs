using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Charecter;

namespace dotnet_rpg.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        private static List<Charecter> charecters = new List<Charecter>{
            new Charecter(), 
            new Charecter {Id = 1, Name = "Sam"}
        }; 
        private readonly IMapper _mapper;
        public CharecterService(IMapper mapper)
        {
            _mapper = mapper;
            
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> AddCharecter(AddCharecterDto newCharecter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharecterDto>>(); 
            Charecter charecter = _mapper.Map<Charecter>(newCharecter); 
            charecter.Id = charecters.Max(c => c.Id) +1; 
            charecters.Add(charecter);
            serviceResponse.Data = charecters.Select(c => _mapper.Map<GetCharecterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> DeleteCharecter(int id)
        {
            ServiceResponse<List<GetCharecterDto>> response = new ServiceResponse<List<GetCharecterDto>>(); 
            try
            {
                Charecter charecter = charecters.First(c => c.Id == id);
                charecters.Remove(charecter); 
                response.Data = charecters.Select(c => _mapper.Map<GetCharecterDto>(c)).ToList(); 
              
            }
            catch(Exception ex)
            { 
                response.Success = false; 
                response.message= ex.Message; 
            }
            return response;  
        }

        public async Task<ServiceResponse<List<GetCharecterDto>>> GetAllCharecters()
        {
            return new ServiceResponse<List<GetCharecterDto>> { 
                    Data =  charecters.Select(c => _mapper.Map<GetCharecterDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharecterDto>> GetCharecterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharecterDto>(); 
            var charecter = charecters.FirstOrDefault(c => c.Id == id); 
            serviceResponse.Data = _mapper.Map<GetCharecterDto>(charecter) ;  
            return serviceResponse; 
        }

        public async Task<ServiceResponse<GetCharecterDto>> UpdateCharecter(UpdateCharecterDto updatedCharecter)
        {
            ServiceResponse<GetCharecterDto> response = new ServiceResponse<GetCharecterDto>(); 
            try
            {
                Charecter charecter = charecters.FirstOrDefault(c => c.Id == updatedCharecter.Id);
                    _mapper.Map(updatedCharecter, charecter); 
                    // charecter.Name = updatedCharecter.Name; 
                    // charecter.HitPoints = updatedCharecter.HitPoints; 
                    // charecter.Strength =updatedCharecter.Strength; 
                    // charecter.Defense = updatedCharecter.Defense; 
                    // charecter.Intelligence = updatedCharecter.Intelligence;
                    // charecter.Class = updatedCharecter.Class ; 
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
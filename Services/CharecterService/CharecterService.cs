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
    }
}
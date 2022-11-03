using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        private static List<Charecter> charecters = new List<Charecter>{
            new Charecter(), 
            new Charecter {Id = 1, Name = "Sam"}
        }; 
        public async Task<ServiceResponse<List<Charecter>>> AddCharecter(Charecter newCharecter)
        {
            var serviceResponse = new ServiceResponse<List<Charecter>>(); 
            charecters.Add(newCharecter); 
            serviceResponse.Data = charecters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Charecter>>> GetAllCharecters()
        {
            return new ServiceResponse<List<Charecter>> { Data = charecters};
        }

        public async Task<ServiceResponse<Charecter>> GetCharecterById(int id)
        {
            var serviceResponse = new ServiceResponse<Charecter>(); 
            var charecter = charecters.FirstOrDefault(c => c.Id == id); 
            serviceResponse.Data = charecter ;  
            return serviceResponse; 
        }
    }
}
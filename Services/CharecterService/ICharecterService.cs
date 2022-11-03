using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharecterService
{
    public interface ICharecterService
    {
        Task<ServiceResponse<List<Charecter>>> GetAllCharecters(); 
        Task<ServiceResponse<Charecter>> GetCharecterById(int id); 
        Task<ServiceResponse<List<Charecter>>> AddCharecter(Charecter newCharecter);    
    }
}
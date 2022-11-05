using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charecter;

namespace dotnet_rpg.Services.CharecterService
{
    public interface ICharecterService
    {
        Task<ServiceResponse<List<GetCharecterDto>>> GetAllCharecters(); 
        Task<ServiceResponse<GetCharecterDto>> GetCharecterById(int id); 
        Task<ServiceResponse<List<GetCharecterDto>>> AddCharecter(AddCharecterDto newCharecter);
        Task<ServiceResponse<GetCharecterDto>> UpdateCharecter(UpdateCharecterDto updatedCharecter);
        Task<ServiceResponse<List<GetCharecterDto>>> DeleteCharecter(int id);
    }
}
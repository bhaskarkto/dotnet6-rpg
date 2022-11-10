using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charecter;
using dotnet_rpg.Dtos.Weapon;
using System.Security.Claims;
using AutoMapper;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;


namespace dotnet_rpg.Services.CharecterService.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        } 
       public async Task<ServiceResponse<GetCharecterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharecterDto> response = new ServiceResponse<GetCharecterDto>(); 
            try{
                Charecter charecter = await _context.Charecters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharecterId &&
                    c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))); 

                if(charecter == null)
                {
                    response.Success = false; 
                    response.message = "Charecter not found"; 
                    return response; 
                }
                
                Weapon weapon = new Weapon
                {
                    Name = newWeapon.Name, 
                    Damage = newWeapon.Damage, 
                    Charecter = charecter
                };
                 
                _context.Weapons.Add(weapon); 
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
    }
}
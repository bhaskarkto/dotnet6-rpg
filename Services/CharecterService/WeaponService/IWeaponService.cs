using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charecter;
using dotnet_rpg.Dtos.Weapon;

namespace dotnet_rpg.Services.CharecterService.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharecterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Charecter;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Charecter, GetCharecterDto>();
            CreateMap<AddCharecterDto, Charecter>();
            CreateMap<UpdateCharecterDto, Charecter>();
            CreateMap<Weapon, GetWeaponDto>(); 
            CreateMap<Skill, GetSkillDto>(); 
        }
    }
}
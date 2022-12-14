using AutoMapper;
using RolePlayingGames.DTO.Characters;
using RolePlayingGames.Models;

namespace RolePlayingGames
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO,Character>();
            CreateMap<UpdateCharacterDTO,Character>();
            
        }
    }
}

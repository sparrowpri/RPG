using AutoMapper;
using RolePlayingGames.DTO.Characters;
using RolePlayingGames.Models;

namespace RolePlayingGames.Services
{
    public class CharacterServices : ICharacterServices
    {
        private readonly IMapper _mapper;   
        public CharacterServices(IMapper mapper)
        {
            _mapper = mapper;
        }
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character
            {
                Id=1,
                Name="walder"
            }
        };
        

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllChar()
        {    var serviceResponse=new ServiceResponse<List<GetCharacterDTO>>{ Data = characters.Select(x=>_mapper.Map<GetCharacterDTO>(x)).ToList()};
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDTO?>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO?> { Data =_mapper.Map<GetCharacterDTO>(( characters.FirstOrDefault(c => c.Id == id)))};
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddChar(AddCharacterDTO newChar)
        {
            var character=_mapper.Map<Character>(newChar);
            character.Id=characters.Max(x=>x.Id)+1;
            characters.Add(character);
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>> { Data = characters.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToList() };
            return serviceResponse;
        }

    }
}

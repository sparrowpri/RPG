using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RolePlayingGames.Data;
using RolePlayingGames.DTO.Characters;
using RolePlayingGames.Models;

namespace RolePlayingGames.Services
{
    public class CharacterServices : ICharacterServices
    {
        private readonly IMapper _mapper; 
        private readonly DataContext _context;
        public CharacterServices(IMapper mapper,DataContext dataContext)
        {
            _mapper = mapper;
            _context = dataContext;
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
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>() ;
            var dbCharacter=await _context.Characters.ToListAsync();
            response.Data = dbCharacter.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToList();
            
            return response;
        }
        public async Task<ServiceResponse<GetCharacterDTO?>> GetCharacterById(int id)
        {
            var response=new ServiceResponse<GetCharacterDTO?>() ;
            response.Data =_mapper.Map<GetCharacterDTO>(await _context.Characters.FirstOrDefaultAsync(x => x.Id == id)) ;
            return response;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddChar(AddCharacterDTO newChar)
        {
            var character=_mapper.Map<Character>(newChar);
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>> { Data = await _context.Characters.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToListAsync() };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO?>> UpdateCharacter(UpdateCharacterDTO updatechar)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var characterToUpdate = await _context.Characters.FirstOrDefaultAsync(x => x.Id == updatechar.Id);
                //characterToUpdate=_mapper.Map<Character>(updatechar);
                characterToUpdate.Name = updatechar.Name;
                characterToUpdate.HitPoints = updatechar.HitPoints;
                characterToUpdate.Strength = updatechar.Strength;
                characterToUpdate.Intelligence = updatechar.Intelligence;
                characterToUpdate.Defence = updatechar.Defence;
                characterToUpdate.Class = updatechar.Class;
                await  _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(characterToUpdate);

            }
            catch (Exception ex)
            {

                serviceResponse.Success=false;
                serviceResponse.Message = ex.Message;
            }    
            
           return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                var characterToDelete = _context.Characters.First(x => x.Id == id);
                _context.Characters.Remove(characterToDelete);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Characters.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Success=false;
                serviceResponse.Message = ex.Message;
                
            }
            return serviceResponse;
            
        }
    }
}

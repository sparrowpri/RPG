using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolePlayingGames.DTO.Characters;
using RolePlayingGames.Models;
using RolePlayingGames.Services;

namespace RolePlayingGames.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private ICharacterServices _characterServices;

        public CharacterController(ICharacterServices characterServices)

        {
            this._characterServices = characterServices;
        }

        [HttpGet("GetAll")]
        //[Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterServices.GetAllChar());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id)
        {
            return Ok(await _characterServices.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDTO newChar)
        {
            
            return Ok(await _characterServices.AddChar(newChar));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updateChar)
        {
            var response = await _characterServices.UpdateCharacter(updateChar);
            if(response==null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> DeleteCharacter(int id)
        {
            var response=await _characterServices.DeleteCharacter(id); 
            if(response==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

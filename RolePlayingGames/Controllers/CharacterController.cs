﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolePlayingGames.Models;

namespace RolePlayingGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character
            {
                Id=1,
                Name="walder"
            }
        };

        [HttpGet("GetAll")]
        //[Route("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }
        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newChar)
        {
            characters.Add(newChar);
            return Ok(characters);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace RPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class characterController : ControllerBase
    {
        private static List<Character> characters=new List<Character>
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
            return Ok(characters.FirstOrDefault(c=>c.Id==id));
        }
    }
}
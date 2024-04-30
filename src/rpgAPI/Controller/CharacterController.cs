using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace rpgAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<Character>>> GetCharacter()
        {
            var serviceResponse = _characterService.GetAllCharacter();
            if (serviceResponse.Data == null || serviceResponse.Data.Count == 0)
            {
                return NotFound();
            }
            return Ok(serviceResponse);
        }


        [HttpGet("id")]
        public ActionResult<ServiceResponse<Character>> GetId(int id)
        {
            var character = _characterService.GetCharacterById(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpPost]
        public ActionResult<ServiceResponse<List<Character>>> PostCharacter(Character newCharacter)
        {
            return Ok(_characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public ActionResult<ServiceResponse<List<Character>>> UpdateCharacter(Character newCharacter)
        {
            return Ok(_characterService.UpdateCharacter(newCharacter));
        }

        [HttpDelete]
        public ActionResult<ServiceResponse<List<Character>>> DeleteCharacter(int id)
        {
            return Ok(_characterService.DeleteCharacter(id));
        }
    }
}
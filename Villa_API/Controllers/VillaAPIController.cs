using Microsoft.AspNetCore.Mvc;
using Villa_API.Data;
using Villa_API.Models;
using Villa_API.Models.DTOs;

namespace Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStorage.villaList);
        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = Ok(VillaStorage.villaList.FirstOrDefault(u => u.Id == id));

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> AddVilla([FromBody] VillaDTO addVilla)
        {
            if (VillaStorage.villaList.FirstOrDefault(u=>u.Name.ToLower() == addVilla.Name.ToLower()) != null) 
            {
                ModelState.AddModelError("","Villa Already Exists");
                return BadRequest(ModelState);
            }

            if (addVilla == null)
            {
                return BadRequest(addVilla);
            }
            if (addVilla.Id > 0)
            {
                return StatusCode(StatusCodes.Status505HttpVersionNotsupported);
            }

            addVilla.Id = VillaStorage.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            VillaStorage.villaList.Add(addVilla);

            //return Ok(addVilla);
            return CreatedAtRoute("GetVilla", new { id = addVilla.Id }, addVilla);

        }
    }
}

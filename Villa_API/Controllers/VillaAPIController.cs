using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa_API.Data;
using Villa_API.Models;
using Villa_API.Models.DTOs;

namespace Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDbContext _dbContext;
        public VillaAPIController(ILogger<VillaAPIController> logger,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting All Villas");
            return Ok(_dbContext.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Villa Error With Id: " + id);
                return BadRequest();
            }
            var villa = Ok(_dbContext.Villas.FirstOrDefault(u => u.Id == id));

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> AddVilla([FromBody] VillaDTO addVilla)
        {
            if (_dbContext.Villas.FirstOrDefault(u => u.Name.ToLower() == addVilla.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa Already Exists");
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

            Villa model = new()
            {
                 Id = addVilla.Id,
                 Name = addVilla.Name,
                 Amenity = addVilla.Amenity,
                 Details = addVilla.Details,
                 ImageUrl = addVilla.ImageUrl,
                 Occupancy = addVilla.Occupancy,
                 Rate = addVilla.Rate,
                 Sqft = addVilla.Sqft
            };

            _dbContext.Villas.Add(model);
            _dbContext.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = addVilla.Id }, addVilla);

        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var deleteId = _dbContext.Villas.FirstOrDefault(u => u.Id == id);

            if (deleteId == null)
            {
                return NotFound();
            }
            _dbContext.Villas.Remove(deleteId);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}",Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id , [FromBody]VillaDTO updateVilla)
        {
            if (updateVilla == null || id != updateVilla.Id)
            {
                return BadRequest();
            }
            //var updateId = _dbContext.Villas.FirstOrDefault(u=>u.Id == id);
            //updateId.Name = updateVilla.Name;
            //updateId.Sqft = updateVilla.Sqft;
            //updateId.Occupancy  = updateVilla.Occupancy;
            Villa model = new()
            {
                Id = updateVilla.Id,
                Name = updateVilla.Name,
                Amenity = updateVilla.Amenity,
                Details = updateVilla.Details,
                ImageUrl = updateVilla.ImageUrl,
                Occupancy = updateVilla.Occupancy,
                Rate = updateVilla.Rate,
                Sqft = updateVilla.Sqft
            };
            _dbContext.Villas.Update(model);
            _dbContext.SaveChanges();
            return NoContent();

        }

        [HttpPatch("{id:int}",Name = "updateParticularVilla")]
        public IActionResult updateParticularVilla(int id, JsonPatchDocument<VillaDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var updateId = _dbContext.Villas.AsNoTracking().FirstOrDefault(u=>u.Id==id);
            VillaDTO model = new()
            {
                Id = updateId.Id,
                Name = updateId.Name,
                Amenity = updateId.Amenity,
                Details = updateId.Details,
                ImageUrl = updateId.ImageUrl,
                Occupancy = updateId.Occupancy,
                Rate = updateId.Rate,
                Sqft = updateId.Sqft
            };

            if (updateId == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(model, ModelState);

            Villa villaModel = new()
            {
                Id = model.Id,
                Name = model.Name,
                Amenity = model.Amenity,
                Details = model.Details,
                ImageUrl = model.ImageUrl,
                Occupancy = model.Occupancy,
                Rate = model.Rate,
                Sqft = model.Sqft
            };

            _dbContext.Villas.Update(villaModel);
            _dbContext.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }

    
}

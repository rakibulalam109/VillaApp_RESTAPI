using Microsoft.AspNetCore.Mvc;
using Villa_API.Models;
using Villa_API.Models.DTOs;

namespace Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController:ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> Get()
        {
            return new List<VillaDTO>
            {
                new VillaDTO{Id=1,Name="Pool View"},
                new VillaDTO{Id=2,Name="Beach View"}
            };
        }
    }
}

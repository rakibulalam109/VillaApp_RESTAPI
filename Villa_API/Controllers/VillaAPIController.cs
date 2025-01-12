using Microsoft.AspNetCore.Mvc;
using Villa_API.Models;

namespace Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController:ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> Get()
        {
            return new List<Villa>
            {
                new Villa{Id=1,Name="Pool View"},
                new Villa{Id=2,Name="Beach View"}
            };
        }
    }
}

using Villa_API.Models.DTOs;

namespace Villa_API.Data
{
    public class VillaStorage
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO{Id=1,Name="Pool View"},
            new VillaDTO{Id=2,Name="Lake View"}
        };
    }
}

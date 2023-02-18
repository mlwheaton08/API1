using API1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [Route("api/[controller]")] //this is the default route when you add a controller. can totally change it if want to
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private static List<District> _districts = new List<District>(); //static word keeps the list there everytime a new method is called, otherwise it would refresh every time

        [HttpGet("{id}")]
        public IActionResult Get([FromQuery]int? id = null, string? name = null) //FromQuery is optional here, but may be needed in rare cases where there's confusion between the variable and the rout (?). There's also FromBody and FromRoute
        {
            var result = _districts;
            if (id != null)
            {
                result = result.Where(d => d.Id == id).ToList();
            }

            if (name != null)
            {
                result = result.Where(d => d.Name == name).ToList();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(District district)
        {
            if (_districts.Any(d => d.Id == district.Id))
            {
                return BadRequest($"Id {district.Id} is already in use.");
            }

            _districts.Add(district);
            return CreatedAtAction("Post", district);
            //return NoContent(); - might see this sometimes
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            District districtToDelete = _districts.Find(d => d.Id == id);
            if (districtToDelete == null)
            {
                return NotFound();
            }

            _districts.Remove(districtToDelete);
            return NoContent();
        }
    }
}
using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _service.GetAllRestaurants();

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant([FromRoute] int id)
        {
            var restaurant = await _service.GetRestaurantById(id);
            if(restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpGet("{id}/menu")]
        public async Task<IActionResult> GetRestaurantWithMenu([FromRoute] int id)
        {
            var restaurant = await _service.GetRestaurantWithDishesById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurantId = await _service.AddNewRestaurant(request);
            return Created($"/api/restaurant/{restaurantId}", restaurantId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _service.UpdateRestaurant(id, request);
            }
            catch
            {
                return Problem("We did not find the resource you requested", "Restaurant", 404);
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            try
            {
                await _service.DeleteRestaurant(id);
            }
            catch
            {
                return Problem("We did not find the resource you requested", "Restaurant", 404);
            }
            return NoContent();
        }

        [HttpPost("{id}/dish")]
        public async Task<IActionResult> AddDish([FromRoute] int id, [FromBody] AddDishRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.AddNewDish(id, request);
            return NoContent();
        }

        [HttpPut("{restaurantId}/dish/{dishId}")]
        public async Task<IActionResult> UpdateDish([FromRoute] int restaurantId, [FromRoute] int dishId, UpdateDishRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _service.UpdateDish(restaurantId, dishId, request);

            if (id == null)
            {
                return Problem("We did not find the resource you requested", "Restaurant or Dish", 404);
            }
            return Created($"api/restaurant/{id}", dishId);
        }

        [HttpDelete("{restaurantId}/dish/{dishId}")]
        public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            int? id;
            try
            {
                id = await _service.DeleteDish(restaurantId, dishId);
            }
            catch
            {
                return Problem("We did not find the resource you requested", "Dish", 404);
            }
            if (id == null)
            {
                return Problem("We did not find the resource you requested", "Restaurant", 404);
            }
            return NoContent();
        }
    }
}

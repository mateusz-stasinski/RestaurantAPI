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
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest request)
        {
            var restaurant = await _service.AddNewRestaurant(request);
            return Created($"/api/restaurant/{restaurant.Id}", restaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantRequest request)
        {
            await _service.UpdateRestaurant(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await _service.DeleteRestaurant(id);
            return NoContent();
        }

        [HttpPost("{id}/dish")]
        public async Task<IActionResult> AddDish([FromRoute] int id, [FromBody] AddDishRequest request)
        {
            await _service.AddNewDish(id, request);
            return NoContent();
        }

        [HttpPut("{restaurantId}/dish/{dishId}")]
        public async Task<IActionResult> UpdateDish([FromRoute] int restaurantId, [FromRoute] int dishId, UpdateDishRequest request)
        {
            await _service.UpdateDish(restaurantId, dishId, request);
            return NoContent();
        }

        [HttpDelete("{restaurantId}/dish/{dishId}")]
        public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await _service.DeleteDish(restaurantId, dishId);
            return NoContent();
        }
    }
}

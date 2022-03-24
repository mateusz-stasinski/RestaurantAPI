using Application.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IEnumerable<GetRestaurantResult>> GetAllRestaurants();
        public Task<GetRestaurantResult> GetRestaurantById(int id);
        public Task<GetRestaurantWithDishesResult> GetRestaurantWithDishesById(int id);
        public Task<int> AddNewRestaurant(AddRestaurantRequest request);
        public Task UpdateRestaurant(int id, UpdateRestaurantRequest request);
        public Task DeleteRestaurant(int id);
        public Task AddNewDish(int restaurantId, AddDishRequest request);
        public Task<int?> UpdateDish(int restaurantId, int dishId, UpdateDishRequest request);
        public Task<int?> DeleteDish(int restaurantId, int dishId);
    }
}

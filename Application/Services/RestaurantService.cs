using Application.Dto;
using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;

        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetRestaurantResult>> GetAllRestaurants()
        {
            var restaurants = await _context.Restaurants.Include(r => r.Address).ToListAsync();
            var restaurantsDto = new List<GetRestaurantResult>();
            foreach(var restaurant in restaurants)
            {
                var restaurantDto = new GetRestaurantResult()
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Category = restaurant.Category,
                    HasDelivery = restaurant.HasDelivery,
                    ContactEmail = restaurant.ContactEmail,
                    ContactNumber = restaurant.ContactNumber,
                    PostalCode = restaurant.Address.PostalCode,
                    City = restaurant.Address.City,
                    Street = restaurant.Address.Street,
                    EstateNumber = restaurant.Address.EstateNumber
                };
                restaurantsDto.Add(restaurantDto);
            }
            return restaurantsDto;
        }

        public async Task<GetRestaurantResult> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.Include(r => r.Address).SingleOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return null;
            }
            else
            {
                var restaurantDto = new GetRestaurantResult()
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Category = restaurant.Category,
                    HasDelivery = restaurant.HasDelivery,
                    ContactEmail = restaurant.ContactEmail,
                    ContactNumber = restaurant.ContactNumber,
                    PostalCode = restaurant.Address.PostalCode,
                    City = restaurant.Address.City,
                    Street = restaurant.Address.Street,
                    EstateNumber = restaurant.Address.EstateNumber
                };
                return restaurantDto;
            }
        }

        public async Task<GetRestaurantWithDishesResult> GetRestaurantWithDishesById(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .SingleOrDefaultAsync(r => r.Id == id);
            var dishes = restaurant.GetDishesByRestaurantId(id);

            var dishesDto = new List<DishDto>();
            foreach (var dish in dishes)
            {
                var dishDto = new DishDto()
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    Price = dish.Price
                };
                dishesDto.Add(dishDto);
            }

            var restaurantDto = new GetRestaurantWithDishesResult()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                ContactEmail = restaurant.ContactEmail,
                ContactNumber = restaurant.ContactNumber,
                PostalCode = restaurant.Address.PostalCode,
                City = restaurant.Address.City,
                Street = restaurant.Address.Street,
                EstateNumber = restaurant.Address.EstateNumber,
                Dishes = dishesDto
            };
            return restaurantDto;
        }
        public async Task<int> AddNewRestaurant(AddRestaurantRequest request)
        {
            var restaurant = new Restaurant()
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Description,
                HasDelivery = request.HasDelivery,
                ContactEmail = request.ContactEmail,
                ContactNumber = request.ContactNumber,
                Address = new Address()
                {
                    PostalCode = request.PostalCode,
                    City = request.City,
                    Street = request.Street,
                    EstateNumber = request.EstateNumber
                }
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return (restaurant.Id);
        }

        public async Task UpdateRestaurant(int id, UpdateRestaurantRequest request)
        {
            var restaurant = await _context.Restaurants.Include(r => r.Address).SingleOrDefaultAsync(r => r.Id == id);
            restaurant.Name = request.Name;
            restaurant.Description = request.Description;
            restaurant.Category = request.Category;
            restaurant.HasDelivery = request.HasDelivery;
            restaurant.ContactEmail = request.ContactEmail;
            restaurant.ContactNumber = request.ContactNumber;
            restaurant.Address.PostalCode = request.PostalCode;
            restaurant.Address.City = request.City;
            restaurant.Address.Street = request.Street;
            restaurant.Address.EstateNumber = request.EstateNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(r => r.Id == id);
            _context.Remove(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewDish(int restaurantId, AddDishRequest request)
        {
            var dish = new Dish()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            var restaurant = await _context.Restaurants
                .Include(r => r.Dishes)
                .SingleOrDefaultAsync(r => r.Id == restaurantId);

            restaurant.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<int?> UpdateDish(int restaurantId, int dishId, UpdateDishRequest request)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Dishes)
                .SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                return null;
            }

            var dish = restaurant.Dishes.SingleOrDefault(d => d.Id == dishId);
            if (dish == null) 
            {
                return null;
            }

            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Price = request.Price;

            await _context.SaveChangesAsync();

            return restaurant.Id;
        }

        public async Task<int?> DeleteDish(int restaurantId, int dishId)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Dishes)
                .SingleOrDefaultAsync(r => r.Id == restaurantId);

            if(restaurant == null)
            {
                return null;
            }

            var dish = restaurant.Dishes.SingleOrDefault(d => d.Id == dishId);
            _context.Remove(dish);
            await _context.SaveChangesAsync();

            return restaurant.Id;
        }
        
    }
}

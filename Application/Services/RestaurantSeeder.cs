using Domain;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _restaurantDbContext;

        public RestaurantSeeder(RestaurantDbContext restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }
        public void Seed()
        {
            if (_restaurantDbContext.Database.CanConnect())
            {
                if (!_restaurantDbContext.Restaurants.Any())
                {
                    var restaurants = CreateSampleRestaurants();
                    _restaurantDbContext.Restaurants.AddRange(restaurants);
                    _restaurantDbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> CreateSampleRestaurants()
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Restaurant 1",
                    Description = "sample restaurant",
                    Category = "Orientall",
                    HasDelivery = true,
                    Address = new Address
                    {
                        City = "Bydgoszcz",
                        Street = "Kaszubska",
                        EstateNumber = "10",
                        PostalCode = "00-000"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Pasta",
                            Description = "Pasta",
                            Price = 10.99
                        },
                        new Dish
                        {
                            Name = "Salmon",
                            Description = "Salmon",
                            Price = 20.99
                        },
                    }
                },
                new Restaurant
                {
                    Name = "Restaurant 2",
                    Description = "sample restaurant",
                    Category = "Organic",
                    HasDelivery = true,
                    Address = new Address
                    {
                        City = "Bydgoszcz",
                        Street = "Kaszubska",
                        EstateNumber = "10",
                        PostalCode = "00-000"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Salat 1",
                            Description = "Salat 1",
                            Price = 10.99
                        },
                        new Dish
                        {
                            Name = "Salat 2",
                            Description = "Salat 2",
                            Price = 20.99
                        },
                    }
                },
                new Restaurant
                {
                    Name = "Restaurant 3",
                    Description = "sample restaurant",
                    Category = "Pizza",
                    HasDelivery = true,
                    Address = new Address
                    {
                        City = "Bydgoszcz",
                        Street = "Kaszubska",
                        EstateNumber = "10",
                        PostalCode = "00-000"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Pizza with chicken",
                            Description = "Pizza with chicken",
                            Price = 10.99
                        },
                        new Dish
                        {
                            Name = "Margaritha",
                            Description = "Margaritha",
                            Price = 20.99
                        },
                    }
                }
            };
            return restaurants;
        }
    }
}

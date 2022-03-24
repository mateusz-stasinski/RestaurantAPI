using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public IEnumerable<Dish> GetDishesByRestaurantId(int id)
        {
            var dishes = Dishes.Where(d => d.RestaurantId == id).ToList();
            return dishes;
        }

        public void AddRestaurant(string name, string description, string category, bool hasDelivery, string contactEmail,
            string contactNumber, string city, string street, string postalCode)
        {
            Name = name;
            Description = description;
            Category = category;
            HasDelivery = hasDelivery;
            ContactEmail = contactEmail;
            ContactNumber = contactNumber;
            Address = new Address()
            {
                City = city,
                Street = street,
                PostalCode = postalCode
            };
        }
    }
}

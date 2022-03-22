using System.Collections.Generic;

namespace Application.Dto
{
    public class GetRestaurantWithDishesResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string EstateNumber { get; set; }
        public IEnumerable<DishDto> Dishes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class UpdateRestaurantRequest
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(6)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(25)]
        public string City { get; set; }
        [Required]
        [MaxLength(35)]
        public string Street { get; set; }
        [Required]
        [MaxLength(6)]
        public string EstateNumber { get; set; }
    }
}

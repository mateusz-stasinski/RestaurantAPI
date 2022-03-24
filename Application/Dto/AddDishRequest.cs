using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class AddDishRequest
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}

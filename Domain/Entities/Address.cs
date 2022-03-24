using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
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
        public string EstateNumber {get;set;}
        

        public virtual Restaurant Restaurant { get; set; }
    }
}

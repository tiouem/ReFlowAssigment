using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Company
    {
        [Required]
        public int companyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; } 

        [Required]
        public string City { get; set; }    

        [Required]
        public string Country { get; set; }     
        
        public string Email { get; set; }
        public int Phone { get; set; }     

        public ICollection<Owner> Owners { get; set; }
    }
}
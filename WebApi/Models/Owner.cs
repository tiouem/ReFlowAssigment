using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Owner
    {
        [Required]
        public int ownerId { get; set; }

        [Required]
        public string Name { get; set; }

        public int companyId { get; set; }

    }
}
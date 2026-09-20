using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models.DTOs
{
    public class OrganisationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        
        public string Description { get; set; }
    }
}

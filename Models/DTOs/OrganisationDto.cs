using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models.DTOs
{
    public class OrganisationDto
    {

        [Required(ErrorMessage ="Name is required")]
        [MaxLength(200, ErrorMessage = "Name must not exceed 200 characters")]
        public string Name { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        [Required]
        public string Type { get; set; }
    }
}

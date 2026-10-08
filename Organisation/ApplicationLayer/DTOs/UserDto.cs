using OrganisationSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.ApplicationLayer.DTOs
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRoles Role { get; set; }

    }
}

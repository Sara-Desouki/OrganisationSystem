using OrganisationSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.ApplicationLayer.DTOs
{
    public class VolunteerDto
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = " Name must not exceed 200 characters ")]
        public string Name { get; set; }


        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(254, ErrorMessage = " Email must not exceed 254 characters ")]
        public string Email { get; set; }


        [MaxLength(20, ErrorMessage = " PhoneNumber must not exceed 20 characters ")]
        [RegularExpression(@"^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        [Required(ErrorMessage = "OrganisationId is required")]
        public int OrgnisationId { get; set; }

    }
}

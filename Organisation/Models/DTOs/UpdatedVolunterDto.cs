using OrganisationSystem.Models.Enums;

namespace OrganisationSystem.Models.DTOs
{
    public class UpdatedVolunteerDto
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int OrganisationId { get; set; }
        public MaritalStatus? Status { get; set; }
    }
}

using OrganisationSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models
{
    public class Volunteer : BaseModel
    {
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrganisationId { get; set; }
        public string PhoneNumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public Organisations Organisation { get; set; }

    }
}

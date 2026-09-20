using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public int OrganationId { get; set; }

        public Organisations Organisation { get; set; }

    }
}

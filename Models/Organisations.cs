using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models
{
    public class Organisations
    {
        public int Id { get; set; }

        public string RefranceId { get; set; }

        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }

      
        public ICollection<Volunteer> volunteers { get; set; }



    }
}

using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models
{
    public class Organisations
    {
        public int Id { get; set; }

        public string RefranceId { get; set; }
       
        public string Name { get; set; }

        public string? Email { get; set; }
        
        public string Type { get; set; }

        public ICollection<Volunteer> volunteers { get; set; }



    }
}

using OrganisationSystem.Domain_Layer.Models;
using OrganisationSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models
{
    public class Organisations : BaseModel
    {
        public string RefranceId { get; set; }
       
        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public string Type { get; private set; }
        public Status Status { get; set; }

        public ICollection<Volunteer> volunteers { get; set; }


        public ICollection<VolunteerOpportunity> VolunteerOpportunities { get; set; }

        public Organisations(string name, string email, string type)
        {
            Name = name;
            Email = email;
            Type = type;
        }

        public void UpdateStatus(int status)
        {
            Status = (Status)status;
        }




    }
}

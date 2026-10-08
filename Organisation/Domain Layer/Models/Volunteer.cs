using OrganisationSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Models
{
    public class Volunteer : BaseModel
    {

        public string ReferenceId { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public int OrganisationId { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateOnly? DateOfBirth { get; private set; }
        public MaritalStatus? MaritalStatus { get; private set; }
        public Organisations Organisation { get;  set; }

    

    public Volunteer( string name, string email, int organisationId, string phoneNumber, DateOnly? dateOfBirth, MaritalStatus? maritalStatus)
        {
            
            Name = name;
            Email = email;
            OrganisationId = organisationId;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            MaritalStatus = maritalStatus;
           
        }


        public void Update(string name, string email, int organisationId, string phoneNumber, DateOnly? dateOfBirth, MaritalStatus? maritalStatus)
        {
            Name = name;
            Email= email;
            OrganisationId = organisationId;
            PhoneNumber= phoneNumber;
            DateOfBirth= dateOfBirth;
            MaritalStatus = maritalStatus;

        }
    }
}
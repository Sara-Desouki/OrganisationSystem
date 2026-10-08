using OrganisationSystem.Domain_Layer.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.ApplicationLayer.DTOs
{
    public class VolunteerOpportunityDto
    {

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title must not exceed 200 characters")]
        public string Title { get;  set; }



        [Required(ErrorMessage = "Description is required")]
        public string Description { get;  set; }



        [Required(ErrorMessage = "OrganisationId is required")]
        public int OrganisationId { get;  set; }


        [MaxLength(300)]
        public string Location { get;  set; }



        [Required(ErrorMessage = "Start Date is required")]
        public DateOnly StartDate { get;  set; }


        [Required(ErrorMessage = "End Date is required")]
        public DateOnly EndDate { get;  set; }



        [Required(ErrorMessage = "RequiredVolunteers is required")]
        public int RequiredVolunteers { get;  set; }


        
        public OpportunityType Type { get;  set; }


        public void ValidStartDate()
        {
            if(StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("StartDate cannot be in the past.");
            }
        }

        public void ValidEndDate() 
        { 
            if(EndDate <= StartDate)
            {
                throw new ArgumentException("EndDate must be after StartDate.");
            }
        }

        public void VaildVolunteerNumber()
        {
            if(RequiredVolunteers < 0)
            {
                throw new ValidationException("Required Volunteers  must be greater than zero.");
            }
        }

    }
}

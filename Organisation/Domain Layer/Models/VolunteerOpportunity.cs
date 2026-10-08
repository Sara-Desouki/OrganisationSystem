using OrganisationSystem.Domain_Layer.Enums;
using OrganisationSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace OrganisationSystem.Domain_Layer.Models
{
    public class VolunteerOpportunity : BaseModel
    {
      

        public VolunteerOpportunity(string title, string description, int organisationId, string location, DateOnly startDate, DateOnly endDate, int requiredVolunteers, OpportunityType type)
        {
            Title = title;
            Description = description;
            OrganisationId = organisationId;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            RequiredVolunteers = requiredVolunteers;
            Type = type;
           
        }

        public string ReferenceId { get; set; }        
        public string Title { get;  private set; }      
        public string Description { get; private set; }

        public int OrganisationId { get;  private set; }

        public string Location { get;  private set; }

        public DateOnly StartDate { get;  private set; }

        public DateOnly EndDate { get;  private set; }

        public int RequiredVolunteers { get; private set; }
        public OpportunityType Type { get; private set; }
        public OpportunityStatus Status { get; set; } = OpportunityStatus.Draft;
        public Organisations Organisation { get; set; }

        internal void Update (string title,
                string description,
                string location,
                int organisationid,
                DateOnly startDate,
                DateOnly endDate,
                int requiredVolunteers,
                OpportunityType type)
        {
            Title = title;
            Description = description;
            Location = location;
            OrganisationId = organisationid;
            StartDate = startDate;
            EndDate = endDate;
            RequiredVolunteers = requiredVolunteers;
            Type = type;
        }

        public void ValidStartDate()
        {
            if (StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("StartDate cannot be in the past.");
            }
        }

        public void ValidEndDate()
        {
            if (EndDate <= StartDate)
            {
                throw new ArgumentException("EndDate must be after StartDate.");
            }
        }

        public void PublishedStatus()
        {
            Status = OpportunityStatus.Published;
        }

        public void CanceledStatus()
        {
            Status = OpportunityStatus.Cancelled;
        }
    }
}

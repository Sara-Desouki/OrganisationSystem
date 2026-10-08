using OrganisationSystem.Domain_Layer.Enums;

namespace OrganisationSystem.ApplicationLayer.DTOs
{
    public class UpdatedOpportunity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReferenceId { get; set; }
        public int OrganisationId { get; set; }
        public OpportunityStatus OpportunityStatus { get; set; }
    }
}

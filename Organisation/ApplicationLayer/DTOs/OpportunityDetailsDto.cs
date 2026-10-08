using OrganisationSystem.Domain_Layer.Enums;

namespace OrganisationSystem.ApplicationLayer.DTOs
{
    public class OpportunityDetailsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReferenceId { get; set; }
        public int OrganisationId { get; set; }
        public string OrganisationReferenceId { get; set; }
        public string OrganisationName { get; set; }
        public OpportunityStatus OpportunityStatus { get; set; }
    }
}

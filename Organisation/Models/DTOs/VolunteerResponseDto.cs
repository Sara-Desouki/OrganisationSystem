namespace OrganisationSystem.Models.DTOs
{
    public class VolunteerResponseDto : BaseModel
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrganisationId { get; set; }
        public string OrganisationReferenceId { get; set; }
        public string OrganisationName { get; set; }

    }
}

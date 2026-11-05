using AutoMapper;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.Domain_Layer.Models;
using OrganisationSystem.Models;

namespace OrganisationSystem.ApplicationLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<VolunteerOpportunity, VolunteerOpportunityDto>().ReverseMap();
            CreateMap<VolunteerDto, Volunteer>().ReverseMap();

        }
    }
}

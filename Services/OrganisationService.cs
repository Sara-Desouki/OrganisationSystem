using OrganisationSystem.Data;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;

namespace OrganisationSystem.Services
{
    public class OrganisationService(GenericRepo<Organisations> orgRepo)
    {

        public Organisations Add(OrganisationDto organisationDto)
        {
            var Org = new Organisations
            {
                Name = organisationDto.Name,
                Email = organisationDto.Email,
                Address = organisationDto.Address,
                Description = organisationDto.Description
            };

               orgRepo.Add(Org);
            return Org;

        }


        public IEnumerable<Organisations> GetByPageSize(int pagesize, int pagenumber)
        {
           return orgRepo.GetByPageSize(pagesize , pagenumber);
        }
    }
}

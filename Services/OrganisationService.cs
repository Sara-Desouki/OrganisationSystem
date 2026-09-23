using Microsoft.AspNetCore.Mvc;
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
                Type = organisationDto.Type
            };

               orgRepo.Add(Org);
            return Org;

        }


        public PagedResult GetByPageSize(int pagesize, int pagenumber)
        {
            int totalCount = orgRepo.GetAll().Count();

            var result = orgRepo.GetByPageSize(pagesize, pagenumber);


            return new PagedResult
            {
                item = result,
                pageNumber = pagenumber,
                pageSize = pagesize,
                totalCount = totalCount
            };
        }
    }
}

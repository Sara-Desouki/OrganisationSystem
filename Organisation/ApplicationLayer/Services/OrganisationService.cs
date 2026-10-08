using Microsoft.AspNetCore.Mvc;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.Models;
using OrganisationSystem.Models.Enums;
using System.Linq.Expressions;

namespace OrganisationSystem.ApplicationLayer.Services
{
    public class OrganisationService(IGenericRepo<Organisations> orgRepo)
    {
        public Organisations Add(OrganisationDto organisationDto)
        {

            var Org = new Organisations(organisationDto.Name, organisationDto.Email, organisationDto.Type);
          

            orgRepo.Add(Org);


            return Org;

        }
        
        public PagedResult GetByPageSize(int pagesize, int pagenumber)
        {
            int totalCount = orgRepo.GetAll().Count();

            var result = orgRepo.GetByPageSize(pagesize, pagenumber , (x => x) );


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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.Models;
using OrganisationSystem.Models.Enums;
using System.Linq.Expressions;

namespace OrganisationSystem.ApplicationLayer.Services
{
    public class OrganisationService(IGenericRepo<Organisations> orgRepo)
    {
        public  async Task<Organisations> AddAsync(OrganisationDto organisationDto)
        {

            var Org = new Organisations(organisationDto.Name, organisationDto.Email, organisationDto.Type);
          

           await orgRepo.AddAsync(Org);


            return Org;

        }
        
        public async Task<PagedResult> GetByPageSizeAsync(int pagesize, int pagenumber)
        {
            int totalCount = await orgRepo.GetAll().CountAsync();

            var result = orgRepo.GetByPageSize(pagesize, pagenumber , (x => x) );


            return new PagedResult
            {
                Items = result,
                pageNumber = pagenumber,
                pageSize = pagesize,
                totalCount = totalCount
            };
        }
    }
}

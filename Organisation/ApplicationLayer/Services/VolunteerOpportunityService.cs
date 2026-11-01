using Microsoft.EntityFrameworkCore;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.Domain_Layer.Enums;
using OrganisationSystem.Domain_Layer.Models;
using OrganisationSystem.Models;
using System.ComponentModel;

namespace OrganisationSystem.ApplicationLayer.Services
{
    public class VolunteerOpportunityService( IGenericRepo<VolunteerOpportunity> opportunityRepo ,
        IGenericRepo<Organisations> orgRepo)
    {
        public async Task<VolunteerOpportunity> AddAsync(VolunteerOpportunityDto opportunityDto)

        {

            if(! await orgRepo.ExistAsync(opportunityDto.OrganisationId))
            {
                throw new ArgumentException("Organisation id not exist ");
            }

            opportunityDto.ValidStartDate();

            opportunityDto.ValidEndDate();

            opportunityDto.VaildVolunteerNumber();

            var volunteerOpportunity = new VolunteerOpportunity(
                opportunityDto.Title,
                opportunityDto.Description,
                opportunityDto.OrganisationId,
                opportunityDto.Location,
                opportunityDto.StartDate,
                opportunityDto.EndDate,
                opportunityDto.RequiredVolunteers,
                opportunityDto.Type
                );

           await opportunityRepo.AddAsync(volunteerOpportunity);

            return volunteerOpportunity ;
        }

        public async Task<( IQueryable<object> , int )> GetByPageSizeAsync(int pageSize , int pageNumber)
        {
            var result = opportunityRepo.GetOpportuntyByPageSize(pageSize , pageNumber 
                ,(x => x.Status == OpportunityStatus.Published)
                ,(x => new
                {
                    id = x.Id ,
                    ReferenceId = x.ReferenceId ,
                    Title = x.Title ,
                    Description = x.Description ,
                    organizationId = x.OrganisationId ,
                    organizationReferenceId = x.Organisation.ReferenceId,
                    organizationName = x.Organisation.Name,
                    startDate = x.StartDate,
                    endDate = x.EndDate,
                    requiredVolunteers = x.RequiredVolunteers,
                    status = x.Status
                }));
            int opportuntyCount = await opportunityRepo.GetOppertuntyCount(x => x.Status == OpportunityStatus.Published).CountAsync();
            return  ( result , opportuntyCount)  ;
        }

        public async  Task<OpportunityDetailsDto> GetByIdAsync(int id)
        {
            
            var result = await opportunityRepo.GetSpecificByIdAsync(id, x => new OpportunityDetailsDto
            {
                Title = x.Title,
                Description = x.Description,
                ReferenceId = x.ReferenceId,
                OrganisationId = x.Organisation.Id,
                OrganisationReferenceId = x.Organisation.ReferenceId,
                OrganisationName = x.Organisation.Name,
                OpportunityStatus = x.Status
            });

            return result ?? throw new KeyNotFoundException($"Opportunity with id {id} was not found."); 
        }

        public async  Task<UpdatedOpportunity>  UpdateOpportuntyAsync(int id , VolunteerOpportunityDto opportunity)

        {
            var volunteerOpportunty = await opportunityRepo.GetByIdAsync(id);

            if (volunteerOpportunty == null)
            {
                throw new ArgumentException("Volunteer Opportunty not found ");
            }
            opportunity.ValidStartDate();
            opportunity.ValidEndDate();
            opportunity.VaildVolunteerNumber();

            if(!await orgRepo.ExistAsync(opportunity.OrganisationId))
            {
                throw new ArgumentException("Organisation id not exist ");
            }

            volunteerOpportunty.Update(opportunity.Title, opportunity.Description, opportunity.Location
                , opportunity.OrganisationId, opportunity.StartDate, opportunity.EndDate, opportunity.RequiredVolunteers, opportunity.Type);


           await opportunityRepo.SaveChangesAsync();

            return new UpdatedOpportunity() 
            {

                Title = volunteerOpportunty.Title,
                Description = volunteerOpportunty.Description,
                ReferenceId =volunteerOpportunty.ReferenceId,
                OrganisationId = volunteerOpportunty.OrganisationId,
                OpportunityStatus = volunteerOpportunty.Status
            };
        }

        public async Task<VolunteerOpportunity> PublishedOpportuntyAsync ( int id)
        {

           var result = await opportunityRepo.GetByIdAsync(id);
            if(result == null)
            {
                throw new ArgumentException("Volunteer Opportunity not found");
            }

            result.ValidStartDate();
            result.ValidEndDate();
            result.PublishedStatus();

           await opportunityRepo.SaveChangesAsync();

            return result;
        }

        public async Task<VolunteerOpportunity> CanceledOppotunityAsync( int id)
        {
            var result = await opportunityRepo.GetByIdAsync(id);
            if (result == null)
            {
                throw new ArgumentException("Volunteer Opportunity not found");
            }
            result.CanceledStatus();

            await opportunityRepo.SaveChangesAsync ();

            return result;
        }
    }
}




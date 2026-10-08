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

        public VolunteerOpportunity Add(VolunteerOpportunityDto opportunityDto)

        {

            if(!orgRepo.Exists(opportunityDto.OrganisationId))
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

            opportunityRepo.Add(volunteerOpportunity);

            return volunteerOpportunity ;
        }

        public ( IQueryable<object> , int ) GetByPageSize(int pageSize , int pageNumber)
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
                    organizationReferenceId = x.Organisation.RefranceId,
                    organizationName = x.Organisation.Name,
                    startDate = x.StartDate,
                    endDate = x.EndDate,
                    requiredVolunteers = x.RequiredVolunteers,
                    status = x.Status
                }));
            int opportuntyCount = opportunityRepo.GetOppertuntyCount(x => x.Status == OpportunityStatus.Published).Count();
            return  ( result , opportuntyCount)  ;
        }

        public OpportunityDetailsDto GetById(int id)
        {
            
            var result = opportunityRepo.GetSpecificById(id, x => new OpportunityDetailsDto
            {
                Title = x.Title,
                Description = x.Description,
                ReferenceId = x.ReferenceId,
                OrganisationId = x.Organisation.Id,
                OrganisationReferenceId = x.Organisation.RefranceId,
                OrganisationName = x.Organisation.Name,
                OpportunityStatus = x.Status
            });

            return result ?? throw new KeyNotFoundException($"Opportunity with id {id} was not found."); ;
        }

        public   UpdatedOpportunity  UpdateOpportunty(int id , VolunteerOpportunityDto opportunity)

        {
            var volunteerOpportunty = opportunityRepo.GetById(id);

            if (volunteerOpportunty == null)
            {
                throw new ArgumentException("Volunteer Opportunty not found ");
            }
            opportunity.ValidStartDate();
            opportunity.ValidEndDate();
            opportunity.VaildVolunteerNumber();

            if(!orgRepo.Exists(opportunity.OrganisationId))
            {
                throw new ArgumentException("Organisation id not exist ");
            }

            volunteerOpportunty.Update(opportunity.Title, opportunity.Description, opportunity.Location
                , opportunity.OrganisationId, opportunity.StartDate, opportunity.EndDate, opportunity.RequiredVolunteers, opportunity.Type);


           opportunityRepo.SaveChanges();

            return new UpdatedOpportunity() 
            {

                Title = volunteerOpportunty.Title,
                Description = volunteerOpportunty.Description,
                ReferenceId =volunteerOpportunty.ReferenceId,
                OrganisationId = volunteerOpportunty.OrganisationId,
                OpportunityStatus = volunteerOpportunty.Status
            };
        }

        public VolunteerOpportunity PublishedOpportunty ( int id)
        {

           var result = opportunityRepo.GetById(id);
            if(result == null)
            {
                throw new ArgumentException("Volunteer Opportunity not found");
            }

            result.ValidStartDate();
            result.ValidEndDate();
            result.PublishedStatus();

            opportunityRepo.SaveChanges();

            return result;
        }

        public VolunteerOpportunity CanceledOppotunity( int id)
        {
            var result = opportunityRepo.GetById(id);
            if (result == null)
            {
                throw new ArgumentException("Volunteer Opportunity not found");
            }
            result.CanceledStatus();

            opportunityRepo.SaveChanges ();

            return result;
        }
    }
}
;



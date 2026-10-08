using Microsoft.EntityFrameworkCore;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.ApplicationLayer.Validator;
using OrganisationSystem.Models;

namespace OrganisationSystem.ApplicationLayer.Services
{
    public class VolunteerService(IGenericRepo<Volunteer> volunteerRepo,
        IGenericRepo<Organisations> orgRepo,
        VolunteerValidator volunteerValidator)
    {

        public Volunteer Add(VolunteerDto volunteerDto)
        {
            DateOnly? date = volunteerDto.DateOfBirth;

            volunteerValidator.ValidateDateOfBirth(date);

            var orgId = volunteerDto.OrgnisationId;
            if (!orgRepo.Exists(orgId))
            {
                throw new ArgumentException("Organisation id not exist ");
            }

            var volunteer = new Volunteer(

                 volunteerDto.Name,
                 volunteerDto.Email,
                 volunteerDto.OrgnisationId,
                 volunteerDto.PhoneNumber,
                 volunteerDto.DateOfBirth,
                 volunteerDto.MaritalStatus
            );

            volunteerRepo.Add(volunteer);


            return volunteer;
        }

        public VolunteerResponseDto GetById(int id)
        {
            var volunteer = volunteerRepo.GetSpecificById(id , x => new VolunteerResponseDto
            {
                Id = x.Id,
                ReferenceId = x.ReferenceId,
                Name = x.Name,
                Email = x.Email,
                OrganisationId = x.OrganisationId,
                OrganisationReferenceId = x.Organisation.RefranceId,
                OrganisationName = x.Organisation.Name,
            });

            return volunteer;
        }

        public UpdatedVolunteerDto UpdateVolunteer(int id, UpdateVolunteerDto updateVolunteerDto)
        {

            var volunteer = volunteerRepo.GetById(id);

            if (volunteer == null)
            {
                throw new ArgumentException("Volunteer not found ");
            }

            DateOnly? date = updateVolunteerDto.DateOfBirth;


            volunteerValidator.ValidateDateOfBirth(date);


            var orgId = updateVolunteerDto.OrganisationId;

            if (!orgRepo.Exists(orgId))
            {
                throw new ArgumentException("Organisation id not exist ");
            }

            volunteer.Update(
               updateVolunteerDto.Name,
               updateVolunteerDto.Email,
               updateVolunteerDto.OrganisationId,   
               updateVolunteerDto.PhoneNumber,      
               updateVolunteerDto.DateOfBirth,      
               updateVolunteerDto.Status);


            try
            {
                volunteerRepo.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var real = ex.InnerException?.Message ?? ex.Message;
                throw new Exception(real);   
            }


            return new UpdatedVolunteerDto()
            {
                Id = volunteer.Id,
                ReferenceId = volunteer.ReferenceId,
                Name = volunteer.Name,
                Email = volunteer.Email,
                PhoneNumber = volunteer.PhoneNumber,
                DateOfBirth = volunteer.DateOfBirth,
                OrganisationId = volunteer.OrganisationId,
                Status = volunteer.MaritalStatus,

            };

        }

        public PagedResult GetByPageSize(int pagesize, int pagenumber)
        {
            int totalCount = volunteerRepo.GetAll().Count();

            var result = volunteerRepo.GetByPageSize(pagesize , pagenumber , (x => new VolunteerResponseDto()
            {
                Id = x.Id,
                ReferenceId = x.ReferenceId,
                Name = x.Name,
                Email = x.Email,
                OrganisationId = x.OrganisationId,
                OrganisationReferenceId  = x.Organisation.RefranceId,
                OrganisationName = x.Organisation.Name,

            }));
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

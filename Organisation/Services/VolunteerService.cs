using OrganisationSystem.Data;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;
using OrganisationSystem.Validator;

namespace OrganisationSystem.Services
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

            var volunteer = new Volunteer()
            {
                Name = volunteerDto.Name,
                Email = volunteerDto.Email,
                OrganisationId = volunteerDto.OrgnisationId,
                PhoneNumber = volunteerDto.PhoneNumber,
                DateOfBirth = volunteerDto.DateOfBirth,
                MaritalStatus = volunteerDto.MaritalStatus
            };

            volunteerRepo.Add(volunteer);


            return volunteer;
        }

        public VolunteerResponseDto GetById(int id)
        {
            var volunteer = volunteerRepo.GetVolunteerById(id);

            if (volunteer == null)
            {
                throw new ArgumentException("Volunteer not found ");
            }
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

            volunteer.Name = updateVolunteerDto.Name;
            volunteer.Email = updateVolunteerDto.Email;
            volunteer.PhoneNumber = updateVolunteerDto.PhoneNumber;
            volunteer.DateOfBirth = updateVolunteerDto.DateOfBirth;
            volunteer.OrganisationId = updateVolunteerDto.OrganisationId;
            volunteer.MaritalStatus = updateVolunteerDto.Status;

            volunteerRepo.SaveChanges();


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

        public PagedResult GetVolunteerByPageSize(int pageSize, int pageNumber)
        {
            var totalVolunteerCount = volunteerRepo.GetAll().Count();

            var result = volunteerRepo.GetVolunteerByPageSize(pageSize, pageNumber);

            return new PagedResult
            {
                item = result,
                totalCount = totalVolunteerCount,
                pageSize = pageSize,
                pageNumber = pageNumber

            };
        }
    }
}

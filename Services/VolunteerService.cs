using Braintree;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.Data;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace OrganisationSystem.Services
{
    public class VolunteerService(GenericRepo<Volunteer> volunteerRepo, GenericRepo<Organisations> orgRepo)
    {

        public Volunteer Add(VolunteerDto volunteerDto)
        {

            var today = DateOnly.FromDateTime(DateTime.Now);
            var minAllowedDate = today.AddYears(-90);
            DateOnly? date = volunteerDto.DateOfBirth;

            if (date.HasValue)
            {
                if (date.Value < minAllowedDate)
                {
                    throw new ArgumentException("Invalid date of birth");
                }
                if (date.Value > today)
                {
                    throw new ArgumentException("Date of birth cannot be in the future");
                }
            }

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
          var volunteer = volunteerRepo.GetVolunteerById(id)
                .Select(x => new VolunteerResponseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReferenceId = x.ReferenceId,
                    Email = x.Email,
                    OrganisationName = x.Organisation.Name,
                    OrganisationId = x.Organisation.Id,
                    OrganisationReferenceId = x.Organisation.RefranceId,

                })
                .FirstOrDefault();

               if (volunteer == null)
            {
                throw new ArgumentException("Volunteer not found ");
            }
            return volunteer;
        }

        public UpdatedVolunteerDto updateVolunteer(int id , UpdateVolunteerDto updateVolunteerDto)
        {

            var volunteer = volunteerRepo.GetById(id);
            if (volunteer == null)
            {
                throw new ArgumentException("Volunteer not found ");
            }

            var today = DateOnly.FromDateTime(DateTime.Now);
            var minAllowedDate = today.AddYears(-90);
            DateOnly? date = updateVolunteerDto.DateOfBirth;

            if (date.HasValue)
            {
                if (date.Value < minAllowedDate)
                {
                    throw new ArgumentException("Invalid date of birth");
                }
                if (date.Value > today)
                {
                    throw new ArgumentException("Date of birth cannot be in the future");
                }
            }

            var orgId = updateVolunteerDto.OrganizationId;
            if (!orgRepo.Exists(orgId))
            {
                throw new ArgumentException("Organisation id not exist ");
            }

            volunteer.Name = updateVolunteerDto.Name;
            volunteer.Email = updateVolunteerDto.Email;
            volunteer.PhoneNumber = updateVolunteerDto.PhoneNumber;
            volunteer.DateOfBirth = updateVolunteerDto.DateOfBirth;
            volunteer.OrganisationId = updateVolunteerDto.OrganizationId;
            volunteer.MaritalStatus =updateVolunteerDto.Status;

            volunteerRepo.SaveChanges();


            return new UpdatedVolunteerDto()
            {
                Id = volunteer.Id,
                 ReferenceId= volunteer.ReferenceId,
                Name = volunteer.Name,
                Email = volunteer.Email,
                PhoneNumber = volunteer.PhoneNumber,
                DateOfBirth =volunteer.DateOfBirth,
                OrganisationId = volunteer.OrganisationId,
                Status = volunteer.MaritalStatus,
                
            };
            
        }

        public PagedResult GetVolunteerByPageSize(int pageSize, int pageNumber)
        {
            var totalVolunteerCount = volunteerRepo.GetAll().Count();

            var result = volunteerRepo.GetByPageSize(pageSize, pageNumber)
                .Select(x => new VolunteerResponseDto
                {
                    Id = x.Id,
                     ReferenceId = x.ReferenceId,
                    Name = x.Name,
                    Email = x.Email,
                    OrganisationId = x.OrganisationId,
                    OrganisationReferenceId = x.Organisation.RefranceId,
                    OrganisationName = x.Organisation.Name
                });

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

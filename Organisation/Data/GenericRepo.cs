using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;

namespace OrganisationSystem.Data
{
    public class GenericRepo<T>(Context context) : IGenericRepo<T>  where T : BaseModel
    {
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();

        }
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }


        public IQueryable<T> GetByPageSize(int pagesize, int pagenumber)
        {

            var result = context.Set<T>()
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize);

            return result;
        }

        public IQueryable<VolunteerResponseDto> GetVolunteerByPageSize(int pagesize, int pagenumber)
        {

            var result = context.Volunteers
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
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

            return result;
        }


        public VolunteerResponseDto GetVolunteerById(int id)
        {

            var result = context.Volunteers
                .Where(x => x.Id == id)
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

            return result;

        }
        public T GetById(int id)
        {
            var result = context.Set<T>()
                .Where(x => x.Id == id)
                .FirstOrDefault();


            return result;

        }

        public bool Exists(int id)
        {
            return context.Set<T>().Any(x => x.Id == id);
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}

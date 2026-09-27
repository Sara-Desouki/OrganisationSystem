using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;

namespace OrganisationSystem.Data
{
    public class GenericRepo<T>(Context context) where T : BaseModel
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

        public IQueryable<T> GetVolunteerById (int id) {

            var result = context.Set<T>()
                .Where(x => x.Id == id);

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

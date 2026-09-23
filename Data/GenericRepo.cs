using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;

namespace OrganisationSystem.Data
{
    public class GenericRepo<T>(Context context) where T : class, new()
    {
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
            try
            {
                context.SaveChanges();
            }

            catch (DbUpdateException ex)
            {
                throw new Exception("This name already exists, please choose another name.", ex);
            }
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
    }
}

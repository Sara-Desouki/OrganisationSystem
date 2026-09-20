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

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    


    public IEnumerable<T> GetByPageSize(int pagesize, int pagenumber)
        {

            var result = context.Set<T>()
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize).ToList();

            return result;
        }
    }
}

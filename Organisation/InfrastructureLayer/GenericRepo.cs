using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.Models;

using System.Linq.Expressions;

namespace OrganisationSystem.InfrastructureLayer
{
    public class GenericRepo<T>(Context context) : IGenericRepo<T> where T : BaseModel
    {
        public async Task AddAsync(T entity)
        {
            context.Set<T>().Add(entity);
            await  context.SaveChangesAsync();

        }
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public  IQueryable<T> GetOppertuntyCount ( Expression<Func <T , bool >> where)
        {
            return context.Set<T>()
                .Where(where); 
        }

        public IQueryable<object> GetByPageSize(int pageSize, int pageNumber,  Expression<Func<T, object>> select)
        {
            IQueryable<object> query = context.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(select);
            return query;

        }

        public async Task<TResult> GetSpecificByIdAsync<TResult>(int id, Expression<Func<T, TResult>> select)
        {
            var result = await context.Set<T>()
                .Where(x => x.Id == id)
                .Select(select)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await context.Set<T>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();


            return result;

        }

        public async Task<bool> ExistAsync(int id)
        {
            return await context.Set<T>().AnyAsync(x => x.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public IQueryable<object> GetOpportuntyByPageSize(int pageSize 
            , int pageNumber ,Expression<Func<T , bool >> where  
            , Expression<Func<T, object>> select)

        {
            var result = context.Set<T>()
                .Where(where)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(select);

            return  result; 

        }

    }
}

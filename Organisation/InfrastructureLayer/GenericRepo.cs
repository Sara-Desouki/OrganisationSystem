using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.Models;

using System.Linq.Expressions;

namespace OrganisationSystem.InfrastructureLayer
{
    public class GenericRepo<T>(Context context) : IGenericRepo<T> where T : BaseModel
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

        public TResult GetSpecificById<TResult>(int id, Expression<Func<T, TResult>> select)
        {
            var result = context.Set<T>()
                .Where(x => x.Id == id)
                .Select(select)
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

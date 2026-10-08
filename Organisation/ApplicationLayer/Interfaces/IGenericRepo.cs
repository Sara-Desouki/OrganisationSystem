using OrganisationSystem.Models;
using System.Linq.Expressions;

namespace OrganisationSystem.ApplicationLayer.Interfaces
{
    public interface IGenericRepo<T> where T : BaseModel
    {
        void Add(T entity);
        bool Exists(int id);
        IQueryable<T> GetAll();
        T GetById(int id);
        void SaveChanges();
        TResult GetSpecificById<TResult>(int id, Expression<Func<T, TResult>> select);
        IQueryable<object> GetByPageSize(int pageSize, int pageNumber,  Expression<Func<T, object>> select);

        public IQueryable<object> GetOpportuntyByPageSize(int pageSize
            , int pageNumber, Expression<Func<T, bool>> where
            , Expression<Func<T, object>> select);

        public IQueryable<T> GetOppertuntyCount(Expression<Func<T, bool>> where);
    }
}
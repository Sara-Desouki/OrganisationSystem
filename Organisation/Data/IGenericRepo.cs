using OrganisationSystem.Models;
using OrganisationSystem.Models.DTOs;

namespace OrganisationSystem.Data
{
    public interface IGenericRepo<T> where T : BaseModel
    {
        void Add(T entity);
        bool Exists(int id);
        IQueryable<T> GetAll();
        T GetById(int id);
        IQueryable<T> GetByPageSize(int pagesize, int pagenumber);
        VolunteerResponseDto GetVolunteerById(int id);
        IQueryable<VolunteerResponseDto> GetVolunteerByPageSize(int pagesize, int pagenumber);
        void SaveChanges();
    }
}
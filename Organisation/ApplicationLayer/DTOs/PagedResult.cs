namespace OrganisationSystem.ApplicationLayer.DTOs
{
    public class PagedResult
    {
        public IQueryable item { get; set; }

        public int totalCount { get; set; }

        public int pageSize { get; set; }

        public int pageNumber { get; set; }
    }
}

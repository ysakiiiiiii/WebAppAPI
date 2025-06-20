using WebApp.APi.Models.Domain;

namespace WebApp.APi.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk>CreateAsync(Walk walk);
        Task<List<Walk>>GetAllAsync(string? filterOn=null, string? filterQuery=null, string? sortBy=null, bool isAscending=true, int pageNumber=1, int pageSize=1000);
    }
}

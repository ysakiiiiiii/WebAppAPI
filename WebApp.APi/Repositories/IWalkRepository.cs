using WebApp.APi.Models.Domain;

namespace WebApp.APi.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk>CreateAsync(Walk walk);
        Task<List<Walk>>GetAllAsync(string? filterOn=null, string? filterQuery=null);
    }
}

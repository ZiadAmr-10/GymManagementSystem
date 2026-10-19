using GymManagementSystem.DAL.Models;
namespace GymManagementSystem.DAL.Repositories.Interfaces;
public interface IPlanRepository : IRepository<Plan>
{
    //// Enable tracking when the entity needs to be updated or deleted.
    

    public Task<IEnumerable<Plan>> GetAllWithMemberShipsAsync(bool tracking = false);
    
}

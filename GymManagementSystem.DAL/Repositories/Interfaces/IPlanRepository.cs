using GymManagementSystem.DAL.Models;
namespace GymManagementSystem.DAL.Repositories.Interfaces;
public interface IPlanRepository
{
    //// Enable tracking when the entity needs to be updated or deleted.
    public Task<IEnumerable<Plan>> GetALLAsync(bool tracking = false);
    public Task<Plan?> GetByIdAsync(int id);
    public Task<int> AddAsync(Plan plan);
    public Task<int> UpdateAsync(Plan plan);
    public Task<int> DeleteAsync(Plan plan);

}

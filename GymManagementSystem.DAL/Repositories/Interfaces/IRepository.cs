using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()  // Requires TEntity to inherit from BaseEntity and have a parameterless constructor.
{
    public Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false);
    public Task<TEntity?> GetByIdAsync(int id);
    public Task AddAsync(TEntity entity);
    public Task UpdateAsync(TEntity entity);
    public Task DeleteAsync(TEntity entity);
}

using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Repositories.Classes;

public class PlanRepository : IPlanRepository
{
    private GymDbContext _dbContext;
    public PlanRepository(GymDbContext dbContext)
    {
        _dbContext= dbContext;
    }
    public async Task<int> AddAsync(Plan plan)
    {
      await _dbContext.Plans.AddAsync(plan);
      return await _dbContext.SaveChangesAsync();  
    }

    public async Task<int> DeleteAsync(Plan plan)
    {
        _dbContext.Plans.Remove(plan);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Plan>> GetALLAsync(bool tracking = false)
    {
        return  tracking ? await _dbContext.Plans.ToListAsync() : await _dbContext.Plans.AsNoTracking().ToListAsync();
    }

    public async Task<Plan?> GetByIdAsync(int id)
    {
        return await _dbContext.Plans.FindAsync(id);
    }

    public async Task<int> UpdateAsync(Plan plan)
    {
        _dbContext.Plans.Update(plan);
        return await (_dbContext.SaveChangesAsync());
    }
}

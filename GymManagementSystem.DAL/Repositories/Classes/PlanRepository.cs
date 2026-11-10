using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Repositories.Classes;

public class PlanRepository : Repository<Plan> , IPlanRepository
{

    public PlanRepository(GymDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<IEnumerable<Plan>> GetAllWithMemberShipsAsync(bool tracking = false)
    {
        return  tracking ? await _dbContext.Plans.Include(x => x.MemberShips).ToListAsync() :await _dbContext.Plans.Include(x => x.MemberShips).AsNoTracking().ToListAsync();

    }

    public async Task<Plan?> GetPlanWithMemberShipsAsync(int id, bool tracking = false)
    {
        return tracking ? await _dbContext.Plans.Include(x => x.MemberShips).FirstOrDefaultAsync(x => x.Id == id) : await _dbContext.Plans.Include(x => x.MemberShips).AsNoTracking().FirstOrDefaultAsync();
    }
}

using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Repositories.Classes
{
    public class MemberRepository : Repository<Member>, IMemberRepository 
    {
        public MemberRepository(GymDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Member?> GetMemberWithRelatedDataAsync(int id)
        {
            return await _dbContext.Members
                          .AsNoTracking()
                          .Include(x => x.MemberShips)
                          .ThenInclude(x => x.Plan)
                          .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

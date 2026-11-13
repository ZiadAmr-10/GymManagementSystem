using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace GymManagementSystem.DAL.Repositories.Classes
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(GymDbContext gymDbContext):base(gymDbContext)
        {
            
        }
        public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync()
        {
            return await _dbContext.Sessions.AsNoTracking().Include(x => x.Trainer).Include(x => x.Category).ToListAsync();
        }

        public async Task<int> GetCountOfBookedSlotsAsync(int SessionId)
        {
            return await _dbContext.MemberSessions.AsNoTracking().Where(x=>x.SessionId==SessionId).CountAsync();
        }
    }
}


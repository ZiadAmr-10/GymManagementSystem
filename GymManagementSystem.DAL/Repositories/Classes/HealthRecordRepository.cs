using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Repositories.Classes
{
    public class HealthRecordRepository : Repository<HealthRecord>, IHealthRecordRepository
    {

        public HealthRecordRepository(GymDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<HealthRecord?> GetHealthRecordAsync(int? memberId)
        {
            return await _dbContext.HealthRecords
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.MemberId == memberId);
        }
    }
}

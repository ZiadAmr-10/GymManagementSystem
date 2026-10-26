using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.DAL.Repositories.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        public Task<Member?> GetMemberWithRelatedDataAsync(int Id);
    }
}

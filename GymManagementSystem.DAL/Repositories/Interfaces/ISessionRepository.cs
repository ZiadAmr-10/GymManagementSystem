using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.DAL.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync();
        Task<int> GetCountOfBookedSlotsAsync(int SessionId);
        Task<Session?> GetSessionWithTrainerAndCategoryAsync(int SessionId);
    }
}

using GymManagementSystem.BLL.Services.Sessions.ViewModels;

namespace GymManagementSystem.BLL.Services.Sessions.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync();
    }
}

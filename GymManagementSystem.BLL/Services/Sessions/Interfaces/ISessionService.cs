using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagmentSystem.BLL.Common;

namespace GymManagementSystem.BLL.Services.Sessions.Interfaces
{
    public interface ISessionService
    {
        Task<Result<IEnumerable<SessionViewModel>>> GetAllSessionsAsync();
    }
}

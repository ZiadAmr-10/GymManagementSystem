using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagmentSystem.BLL.Common;

namespace GymManagementSystem.BLL.Services.Sessions.Interfaces
{
    public interface ISessionService
    {
        Task<Result<IEnumerable<SessionViewModel>>> GetAllSessionsAsync();
        Task<Result> CreateSessionAsync(CreateSessionViewModel model);
        public Task<Result<IEnumerable<TrainerSelectViewModel>>> GetTrainersForDropDownAsync();
        public Task<Result<IEnumerable<CategorySelectViewModel>>> GetCategoriesForDropDownAsync();
        public Task<Result<SessionDetailsViewModel>> GetSessionDetailsAsync(int id);
        public Task<Result<SessionToUpdateViewModel>> GetSessionToUpdateAsync(int? id);
        public Task<Result> UpdateSessionAsync(SessionToUpdateViewModel? model);
        Task<Result> DeleteSessionAsync(int? id);
    }
}

using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Sessions.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync()
        {
           var sessions = await _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategoryAsync();

            var MappedSession = sessions.Select(s => new SessionViewModel()
            {
                Id = s.Id,
                Capacity = s.Capacity,
                CategoryName=s.Category.CategoryName,
                Description=s.Description,
                TrainerName=s.Trainer.Name,
                StartDate= s.StartDate,
                EndDate=s.EndDate
            });
            foreach (var session in MappedSession)
            {
                session.AvailableSlots = session.Capacity - await _unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id);
            }
            return MappedSession;
        }
    }
}

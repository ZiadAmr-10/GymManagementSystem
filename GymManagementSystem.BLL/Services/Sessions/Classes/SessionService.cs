using AutoMapper;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Sessions.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync()
        {
           var sessions = await _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategoryAsync();

            var mappedSession = _mapper.Map<IEnumerable<SessionViewModel>>(sessions); 

            foreach (var session in mappedSession)
            {
                session.AvailableSlots = session.Capacity - await _unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id);
            }
            return mappedSession;
        }
    }
}

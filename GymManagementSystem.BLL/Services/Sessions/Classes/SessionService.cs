using AutoMapper;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Models.Enums;
using GymManagementSystem.DAL.Repositories.Interfaces;
using GymManagmentSystem.BLL.Common;

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

        public async Task<Result<IEnumerable<SessionViewModel>>> GetAllSessionsAsync()
        {
           var sessions = await _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAndCategoryAsync();
           

            var mappedSession = _mapper.Map<IEnumerable<SessionViewModel>>(sessions); 

            foreach (var session in mappedSession)
            {
                session.AvailableSlots = session.Capacity - await _unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id);
            }

            return Result<IEnumerable<SessionViewModel>>.OK(mappedSession);
           
        }

        public async Task<Result> CreateSessionAsync(CreateSessionViewModel model)
        {
            try
            {
                if (model.EndDate <= model.StartDate)
                    return Result.Validation("EndDate must be after start date");
                if (model.StartDate <= DateTime.Now)
                    return Result.Validation("StartDate must be in the future");
                if (model.Capacity < 1 || model.Capacity > 25)
                    return Result.Validation("Capacity must be between 1 - 25");

                var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.TrainerId);
                if (trainer == null)
                    return Result.NotFound("Trainer NotFound");
                var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId);
                if (category == null)
                    return Result.NotFound("Category NotFound");

                var isValid = Enum.TryParse<Specialty>(category.CategoryName, true, out var categorySpecialty);

                if (!isValid || trainer.Specialty != categorySpecialty)
                    return Result.Validation("Trainer specialty does not match the selected category");

                var session = _mapper.Map<Session>(model);
                await _unitOfWork.GetRepository<Session>().AddAsync(session);
                var result = await _unitOfWork.SaveChangesAsync() > 0;

                return result ? Result.OK() : Result.Fail("Failed to create session");
            }
            catch
            {
                return Result.Fail("Internal Server Error");
            }
        }

        public async Task<Result<IEnumerable<TrainerSelectViewModel>>> GetTrainersForDropDownAsync()
        {
            var result = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();

            var model = _mapper.Map<IEnumerable<TrainerSelectViewModel>>(result);

            return Result<IEnumerable<TrainerSelectViewModel>>.OK(model);
        }

        public async Task<Result<IEnumerable<CategorySelectViewModel>>> GetCategoriesForDropDownAsync()
        {
            var result = await _unitOfWork.GetRepository<Category>().GetAllAsync();

            var model = _mapper.Map<IEnumerable<CategorySelectViewModel>>(result);

            return Result<IEnumerable<CategorySelectViewModel>>.OK(model);
        }

        public async Task<Result<SessionDetailsViewModel>> GetSessionDetailsAsync(int id)
        {
            try
            {
                var session = await _unitOfWork.sessionRepository.GetSessionWithTrainerAndCategoryAsync(id);
                if (session == null)
                    return Result<SessionDetailsViewModel>.NotFound();

                var sessionDetails = _mapper.Map<SessionDetailsViewModel>(session);

                sessionDetails.BookedSlots = await _unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(id);

                return Result<SessionDetailsViewModel>.OK(sessionDetails);
            }
            catch
            {
                return Result<SessionDetailsViewModel>.Fail("Internal Server Error");
            }
        }
    }
}

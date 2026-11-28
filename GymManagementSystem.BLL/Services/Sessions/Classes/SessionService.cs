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

        public async Task<Result<SessionToUpdateViewModel>> GetSessionToUpdateAsync(int? id)
        {
            try
            {
                if (!id.HasValue || id.Value <= 0)
                    return Result<SessionToUpdateViewModel>.NotFound();

                var session = await _unitOfWork.sessionRepository.GetSessionWithTrainerAndCategoryAsync(id.Value);

                if (session == null)
                    return Result<SessionToUpdateViewModel>.NotFound();

                var mappedSession = _mapper.Map<SessionToUpdateViewModel>(session);

                return Result<SessionToUpdateViewModel>.OK(mappedSession);
            }
            catch
            {
                return Result<SessionToUpdateViewModel>.Fail("Failed to retrieve session for editing");
            }
        }

        public async Task<Result> UpdateSessionAsync(SessionToUpdateViewModel? model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    return Result.Fail("Model Not Found ");

                if (!model.TrainerId.HasValue || model.TrainerId.Value <= 0)
                    return Result.Validation("Trainer is required");

                if (!model.StartDate.HasValue || !model.EndDate.HasValue)
                    return Result.Validation("Start and End dates are required");

                var session = await _unitOfWork.sessionRepository.GetByIdAsync(model.Id);

                if (session == null)
                    return Result.NotFound();
                if (session.StartDate <= DateTime.Now)
                {
                    return Result.Fail("Only upcoming sessions can be edited");
                }

                if (model.EndDate.Value <= model.StartDate.Value)
                {
                    return Result.Fail("EndDate must be After Start Date");
                }

                if (model.StartDate.Value <= DateTime.Now)
                {
                    return Result.Fail("StartDate must be In The Future");
                }

                var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.TrainerId.Value);

                if (trainer == null)
                    return Result.Fail("Trainer Not Found");

                var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(session.CategoryId);

                if (category == null)
                    return Result.Fail("Category Not Found");

                bool isValid = Enum.TryParse<Specialty>(category.CategoryName, true, out var categorySpecialty);

                if (!isValid || trainer.Specialty != categorySpecialty)
                    return Result.Fail("Trainer does not match the session category");

                var sessions = await _unitOfWork.sessionRepository.GetAllAsync();

                bool isTrainerNotAvailable = sessions.Any(x => x.TrainerId == model.TrainerId.Value
                                                          && x.Id != model.Id
                                                          && x.StartDate < model.EndDate.Value
                                                          && x.EndDate > model.StartDate.Value);

                if (isTrainerNotAvailable)
                    return Result.Fail("Trainer not available this time");

                session.TrainerId = model.TrainerId.Value;
                session.Description = model.Description;
                session.StartDate = model.StartDate.Value;
                session.EndDate = model.EndDate.Value;
                await _unitOfWork.sessionRepository.UpdateAsync(session);
                var result = await _unitOfWork.SaveChangesAsync() > 0;

                if (result)
                    return Result.OK();
                else
                    return Result.Fail("Faild To update the session");
            }
            catch
            {
                return Result.Fail("Internal Server Error");
            }

        }
        public async Task<Result> DeleteSessionAsync(int? id)
        {
            try
            {
                if (!id.HasValue || id.Value <= 0)
                    return Result.Fail("Invalid Id");

                var session = await _unitOfWork.sessionRepository.GetByIdAsync(id.Value);

                if (session == null)
                    return Result.Fail("Session Not Found");

                if (session.StartDate <= DateTime.Now && session.EndDate >= DateTime.Now)
                    return Result.Fail("Cannot delete an ongoing session");

                await _unitOfWork.sessionRepository.DeleteAsync(session);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                if (result)
                    return Result.OK();

                return Result.Fail("Session Cannot be Deleted");
            }
            catch
            {
                return Result.Fail("Internal Server Error");
            }
        }
    }
}

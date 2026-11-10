using GymManagementSystem.BLL.Services.Plans.Interfaces;
using GymManagementSystem.BLL.Services.Plans.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Plans.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PlanViewModel>> GetPlansAsync()
        {
            var plans = await _unitOfWork.planRepository.GetAllAsync();
            return plans.Select(x => new PlanViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                DurationDays = x.DurationDays,
                IsActive = x.IsActive
            });
        }
        public async Task<PlanDetailsViewModel?> GetPlanDetailsAsync(int? id)
        {
            if (id == null || id <= 0)
                return null;
            var plan =await _unitOfWork.planRepository.GetByIdAsync(id.Value);
            if (plan == null)
                return null;
            var model = new PlanDetailsViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                Description = plan.Description,
                IsActive = plan.IsActive
            };
            return model;
        }

        public async Task<PlanToUpdateViewModel?> GetPlanToUpdateAsync(int? id)
        {
            if (id == null || id <= 0)
                return null;
            var plan = await _unitOfWork.planRepository.GetByIdAsync(id.Value);
            if (plan == null)
                return null;
            return new PlanToUpdateViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
            };
        }

        public async Task<bool> UpdatePlanAsync(PlanToUpdateViewModel model)
        {
            var plan = await _unitOfWork.planRepository.GetPlanWithMemberShipsAsync(model.Id);
            if (plan == null)
                return false;
            if (plan.MemberShips.Any(x => x.IsActive))
                return false;

            if (model.Name!=plan.Name)
                return false;
            plan.Price=model.Price;
            plan.DurationDays=model.DurationDays;
            plan.Description=model.Description;
            await _unitOfWork.planRepository.UpdateAsync(plan);
          return  await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}

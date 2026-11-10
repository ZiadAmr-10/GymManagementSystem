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
    }
}

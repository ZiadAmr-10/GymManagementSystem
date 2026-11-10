using GymManagementSystem.BLL.Services.Plans.ViewModels;

namespace GymManagementSystem.BLL.Services.Plans.Interfaces
{
    public interface IPlanService
    {
        Task <IEnumerable<PlanViewModel>> GetPlansAsync();
        public Task<PlanDetailsViewModel?> GetPlanDetailsAsync(int? id);
        Task<PlanToUpdateViewModel?> GetPlanToUpdateAsync(int? id);
        Task<bool> UpdatePlanAsync(PlanToUpdateViewModel model);
        Task<bool> TogglePlanStatusAsync(int? id);
    }
}

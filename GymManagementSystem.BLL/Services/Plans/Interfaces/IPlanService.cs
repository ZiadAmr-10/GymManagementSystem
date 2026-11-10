using GymManagementSystem.BLL.Services.Plans.ViewModels;

namespace GymManagementSystem.BLL.Services.Plans.Interfaces
{
    public interface IPlanService
    {
        Task <IEnumerable<PlanViewModel>> GetPlansAsync();
        Task<PlanDetailsViewModel?> GetPlanDetailsAsync(int? id);
    }
}

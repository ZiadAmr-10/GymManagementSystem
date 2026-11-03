using GymManagementSystem.BLL.Services.Trainers.ViewModels;

namespace GymManagementSystem.BLL.Services.Trainers.Interface
{
    public interface ITrainerService
    {
        public Task<IEnumerable<TrainerViewModel>> GetTrainersAsync();
    }
}

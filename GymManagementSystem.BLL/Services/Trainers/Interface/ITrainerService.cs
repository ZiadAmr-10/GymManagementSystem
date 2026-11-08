using GymManagementSystem.BLL.Services.Trainers.ViewModels;

namespace GymManagementSystem.BLL.Services.Trainers.Interface
{
    public interface ITrainerService
    {
        public Task<IEnumerable<TrainerViewModel>> GetTrainersAsync();

        public Task<bool> AddTrainerAsync(CreateTrainerViewModel model);
        public Task<TrainerDetailsViewModel?> GetTrainerById(int? id);
        public Task<TrainerToUpdateViewModel?> GetTrainerToUpdateAsync(int? id);
        public Task<bool> UpdateTrainerAsync(TrainerToUpdateViewModel model);
        public Task<bool> DeleteTrainerAsync(int? id);
    }
}

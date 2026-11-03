using GymManagementSystem.BLL.Services.Trainers.Interface;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Trainers.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TrainerViewModel>> GetTrainersAsync()
        {
           var trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync();

            return trainers.Select(x => new TrainerViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialty.ToString()
            });
        }
    }
}

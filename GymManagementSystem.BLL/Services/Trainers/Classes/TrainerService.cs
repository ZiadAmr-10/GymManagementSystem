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

        public async Task<bool> AddTrainerAsync(CreateTrainerViewModel model)
        {
            try
            {
                var trainer = new Trainer()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Gender = model.Gender,
                    Specialty = model.Specialty,
                    DateOfBirth = model.DateOfBirth.Value,
                    Address = new Address
                    {
                        BuildingNumber=model.BuildingNumber,
                        Street = model.Street,
                        City = model.City,
                    }
                };
                await _unitOfWork.GetRepository<Trainer>().AddAsync(trainer);
                return await _unitOfWork.SaveChangesAsync() > 0 ;
            }
            catch
            {
                return false;
            }
        }

    }
}

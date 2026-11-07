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

        public async Task<TrainerDetailsViewModel?> GetTrainerById(int? id)
        {
            if (id is null || id <= 0)
                return null; 
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id.Value);

            if (trainer == null)
                return null;
            var model = new TrainerDetailsViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialty.ToString(),
                DateOfBirth = trainer.DateOfBirth.ToString("MM/dd/yyyy"),
                Address = $"{trainer.Address?.BuildingNumber ?? 0} - {trainer.Address?.Street ?? "Not Found"} - {trainer.Address?.City ?? "Not Found"}"
            };
            return model;
        }
    }
}

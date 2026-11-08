using FluentValidation;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Models.Enums;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Trainers.Validators
{
    public class TrainerViewModelToUpdateValidator : AbstractValidator<TrainerToUpdateViewModel>
    {
        public TrainerViewModelToUpdateValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MustAsync(async (model, email, cancellation) =>
                {
                    var trainers = await unitOfWork.GetRepository<Trainer>().GetAllAsync();

                    return !trainers.Any(x => x.Email == email && x.Id != model.Id);
                })
                .WithMessage("This email is already used by another trainer.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^(010|011|012|015)\d{8}$")
                .WithMessage("Invalid Egyptian phone number.")
                .MustAsync(async (model, phone, cancellation) =>
                {
                    var trainers = await unitOfWork.GetRepository<Trainer>().GetAllAsync();

                    return !trainers.Any(x => x.Phone == phone && x.Id != model.Id);
                })
                .WithMessage("This phone number is already used by another trainer.");

            RuleFor(x => x.BuildingNumber)
                              .Cascade(CascadeMode.Stop)
                              .NotEmpty()
                              .WithMessage("BuildingNumber Is Required !")
                              .InclusiveBetween(1, 9000)
                              .WithMessage("Building number must be between 1 and 9000");

            RuleFor(x => x.Street)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("Street Is Required!")
                   .Length(2, 150)
                   .WithMessage("Street must be between 2 and 150 characters")
                   .Matches(@"^[a-zA-Z0-9\s]+$")
                   .WithMessage("Street can only contain letters, numbers and spaces");

            RuleFor(x => x.City)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("City Is Required!")
                   .Length(2, 100)
                   .WithMessage("City must be between 2 and 100 characters")
                   .Matches(@"^[a-zA-Z\s]+$")
                   .WithMessage("City Can Only Contain Letters and Spaces");

            RuleFor(x => x.Specialty)
                .IsInEnum()
                .WithMessage("Please select a valid specialty.");
        }
    }
}
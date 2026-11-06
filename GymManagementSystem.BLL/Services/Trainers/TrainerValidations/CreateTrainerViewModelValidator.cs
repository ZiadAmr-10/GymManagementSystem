using FluentValidation;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Trainers.TrainerValidations
{
    public class CreateTrainerViewModelValidator : AbstractValidator<CreateTrainerViewModel>
    {
        private readonly IRepository<Trainer> _trainerRepository;
        public CreateTrainerViewModelValidator(IRepository<Trainer> trainerRepository)
        {
            _trainerRepository = trainerRepository;

            RuleFor(x=>x.Name)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("Name Is Required !")
                   .Matches(@"^[a-zA-Z\s]+$")
                   .WithMessage("Name Can Only Contain Letters and Spaces");

            RuleFor(x => x.Email)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("Email Is Required !")
                   .EmailAddress()
                   .WithMessage("Invalid Email Format")
                    .MustAsync(async (email, CancellationToken) =>
                    {
                        var trainers = await _trainerRepository.GetAllAsync();
                        return !trainers.Any(x => x.Email == email);
                    })
                    .WithMessage("Email already exists!");

            RuleFor(x => x.Phone)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage("Phone Is Required")
                    .Matches(@"^(010|012|011|015)\d{8}$")
                    .WithMessage("Phone number must be a valid Egyptian mobile number")
                    .MustAsync(async (phone, CancellationToken) =>
                    {
                        var trainers = await _trainerRepository.GetAllAsync();
                        return !trainers.Any(x => x.Phone == phone);
                    })
                    .WithMessage("Phone number already exists!");

            RuleFor(x=>x.DateOfBirth)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty()
                  .WithMessage("Date Of Birth Is Required !")
                  .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                  .WithMessage("Date of birth cannot be in the future");

            RuleFor(x => x.Gender)
                 .IsInEnum()
                 .WithMessage("Invalid Gender");

            RuleFor(x => x.Specialty)
                   .IsInEnum()
                   .WithMessage("Invalid Specialty");

            RuleFor(x=>x.BuildingNumber)
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

        }


    }
}

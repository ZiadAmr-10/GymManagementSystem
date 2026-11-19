using FluentValidation;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;

namespace GymManagementSystem.BLL.Services.Sessions.SessionValidations
{
    public class CreateSessionValidator : AbstractValidator<CreateSessionViewModel> 
    {
        public CreateSessionValidator()
        {
            RuleFor(x => x.Description)
                   .NotEmpty()
                   .WithMessage("Description is required !")
                   .Length(10,500)
                   .WithMessage("Description must be between 10 - 500 characters");

            RuleFor(x => x.Capacity)
                   .InclusiveBetween(1, 25)
                   .WithMessage("Capacity must be between 1 and 25");

            RuleFor(x => x.StartDate)
                   .NotEmpty()
                   .WithMessage("Start date is required");
            
            RuleFor(x => x.EndDate)
                  .NotEmpty()
                  .WithMessage("End date is required")
                  .GreaterThan(x => x.StartDate)
                  .WithMessage("End date must be after start date");

            RuleFor(x => x.TrainerId)
                   .GreaterThan(0)
                   .WithMessage("Trainer is required");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Category is required");
                
        }
    }
}

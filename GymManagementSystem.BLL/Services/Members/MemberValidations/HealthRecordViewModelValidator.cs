using FluentValidation;
using GymManagementSystem.BLL.Services.Members.ViewModels;

namespace GymManagementSystem.BLL.Services.Members.MemberValidations
{
    public class HealthRecordViewModelValidator : AbstractValidator<HealthRecordViewModel>
    {
        public HealthRecordViewModelValidator()
        {
            RuleFor(x => x.Height)
                   .InclusiveBetween(0.1m, 300m)
                   .WithMessage("Height must be greater than 0");

            RuleFor(x => x.Weight)
                    .InclusiveBetween(0.1m, 500m)
                    .WithMessage("Weight must be greater than 0");

            RuleFor(x => x.BloodType)
                   .NotEmpty()
                   .WithMessage("Blood type is required!")
                   .MaximumLength(3)
                   .WithMessage("Blood type must be 3 characters or less");
        }
    }
}

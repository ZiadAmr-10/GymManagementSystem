using FluentValidation;
using GymManagementSystem.BLL.Services.Plans.ViewModels;

public class PlanToUpdateViewModelValidator : AbstractValidator<PlanToUpdateViewModel>
{
    public PlanToUpdateViewModelValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.DurationDays)
            .GreaterThan(0)
            .WithMessage("Duration must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
    }
}
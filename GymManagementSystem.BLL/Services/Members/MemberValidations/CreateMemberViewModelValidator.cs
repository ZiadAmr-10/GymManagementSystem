using FluentValidation;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Members.MemberValidations
{
    public class CreateMemberViewModelValidator : AbstractValidator<CreateMemberViewModel>
    {
        private readonly IRepository<Member> _memberRepository;
        public CreateMemberViewModelValidator(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;

            RuleFor(x => x.Name)
                   .Cascade(CascadeMode.Stop) //// Stops executing the remaining validation rules for this property when one rule fails.
                   .NotEmpty()
                   .WithMessage("Name Is Required!")
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
                       var members = await _memberRepository.GetAllAsync();
                       return !members.Any(x => x.Email == email);
                   } )
                   .WithMessage("Email already exists!");

            RuleFor(x => x.Phone)
                   .Cascade(CascadeMode.Stop) 
                   .NotEmpty()
                   .WithMessage("Phone Is Required!")
                   .Matches(@"^(010|011|012|015)\d{8}$")
                   .WithMessage("Phone number must be a valid Egyptian mobile number")
                   .MustAsync(async (phone, cancellationToken) =>
                   {
                       var members = await _memberRepository.GetAllAsync();

                       return !members.Any(m => m.Phone == phone);
                   })
                   .WithMessage("Phone number already exists!");

            RuleFor(x => x.DateOfBirth)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("Date Of Birth Is Required !")
                   .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                   .WithMessage("Date of birth cannot be in the future");

            RuleFor(x => x.Gender)
                   .IsInEnum()
                   .WithMessage("Invalid Gender");

            RuleFor(x => x.BuildNumber)
                   .InclusiveBetween(1, 9000)
                   .WithMessage("Building number must be between 1 and 9000");

            RuleFor(x => x.City)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage("City Is Required!")
                    .Length(2, 100)
                    .WithMessage("City must be between 2 and 100 characters")
                    .Matches(@"^[a-zA-Z\s]+$")
                    .WithMessage("City Can Only Contain Letters and Spaces");

            RuleFor(x => x.Street)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage("Street Is Required!")
                    .Length(2, 150)
                    .WithMessage("Street must be between 2 and 150 characters")
                    .Matches(@"^[a-zA-Z0-9\s]+$")
                    .WithMessage("Street can only contain letters, numbers and spaces");

            RuleFor(x => x.HealthRecordViewModel)
                   .NotNull()
                   .SetValidator(new HealthRecordViewModelValidator());
        }
    }
}

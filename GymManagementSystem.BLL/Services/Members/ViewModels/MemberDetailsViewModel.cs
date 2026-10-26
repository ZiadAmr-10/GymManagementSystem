namespace GymManagementSystem.BLL.Services.Members.ViewModels
{
    public class MemberDetailsViewModel
    {

        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string? Photo { get; set; }
        public string? PlanName { get; set; }
        public string? MemberShipStartDate { get; set; }
        public string?  MemberShipEndDate { get; set; }

        public string DateOfBirth { get; set; }
        public string Address { get; set; } = default!;
    }
}

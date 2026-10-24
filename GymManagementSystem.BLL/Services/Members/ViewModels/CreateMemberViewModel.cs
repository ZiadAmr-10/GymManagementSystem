using GymManagementSystem.DAL.Models.Enums;
namespace GymManagementSystem.BLL.Services.Members.ViewModels;

public class CreateMemberViewModel
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int BuildNumber { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public HealthRecordViewModel HealthRecordViewModel { get; set; } = default!;

}

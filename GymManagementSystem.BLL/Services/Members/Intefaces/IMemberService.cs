using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.BLL.Services.Members.Intefaces;

public interface IMemberService
{
    public Task<IEnumerable<MemberViewModel>> GetMembersAsync();
    public Task<bool> AddMemberAsync(CreateMemberViewModel model);
    public Task<MemberDetailsViewModel?> GetMemberAsync(int? Id);
    public Task<HealthRecordViewModel?> GetHealthRecordAsync(int? Id);
    public Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int? Id);
    public Task<bool>UpdateMemberAsync(MemberToUpdateViewModel model);
}

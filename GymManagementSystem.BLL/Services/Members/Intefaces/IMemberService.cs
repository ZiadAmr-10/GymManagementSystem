using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagmentSystem.BLL.Common;

namespace GymManagementSystem.BLL.Services.Members.Intefaces;

public interface IMemberService
{
    public Task<Result<IEnumerable<MemberViewModel>>> GetMembersAsync();
    public Task<Result> AddMemberAsync(CreateMemberViewModel model);
    public Task<Result<MemberDetailsViewModel?>> GetMemberAsync(int? Id);
    public Task<HealthRecordViewModel?> GetHealthRecordAsync(int? Id);
    public Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int? Id);
    public Task<bool>UpdateMemberAsync(MemberToUpdateViewModel model);
    public Task<bool> DeleteMemberAsync(int? Id);
}

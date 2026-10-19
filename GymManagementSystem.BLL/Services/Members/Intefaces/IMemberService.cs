using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.BLL.Services.Members.Intefaces;

public interface IMemberService
{
    public Task<IEnumerable<Member>> GetMembersAsync();
}

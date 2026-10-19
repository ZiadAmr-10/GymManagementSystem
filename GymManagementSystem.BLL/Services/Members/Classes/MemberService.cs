using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Members.Classes;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _membersRepository;
    public MemberService(IRepository<Member> membersRepository)
    {
        IRepository<Member> _membersRepository = membersRepository;
    }
    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        return await _membersRepository.GetAllAsync();
    }
}

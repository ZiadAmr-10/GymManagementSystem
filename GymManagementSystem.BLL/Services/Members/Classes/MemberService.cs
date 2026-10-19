using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
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
    public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
    {
        var members = await _membersRepository.GetAllAsync();
        return members.Select(x => new MemberViewModel()
        {
            id=x.Id,
            Name=x.Name,
            Email=x.Email,
            Phone=x.Phone,
            Gender=x.Gender.ToString(),
            Photo=x.Photo,
        });
    }
}

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
        _membersRepository = membersRepository;
    }

    public async Task<bool> AddMemberAsync(CreateMemberViewModel model)
    {
        try
        {
            var Member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth.Value,
                Gender = model.Gender,
                Phone = model.Phone,
                Address = new Address()
                {
                    Street = model.Street,
                    City = model.City,
                    BuildingNumber = model.BuildNumber
                },
                HealthRecord = new HealthRecord()
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Height = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    Note = model.HealthRecordViewModel.Note,
                }

            };

            return await _membersRepository.AddAsync(Member) > 0;
        }
        catch
        {
            return false;
        }
       

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

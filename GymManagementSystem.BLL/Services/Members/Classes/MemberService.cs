using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Members.Classes;

public class MemberService : IMemberService 
{
    private readonly IMemberRepository _memberRepository;
    public MemberService(IMemberRepository membersRepository)
    {
        _memberRepository = membersRepository;
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

            return await _memberRepository.AddAsync(Member) > 0;
        }
        catch
        {
            return false;
        }
       

    }

    public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
    {
        var members = await _memberRepository.GetAllAsync();
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

    public async Task<MemberDetailsViewModel?> GetMemberAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return null;

        var member = await _memberRepository.GetMemberWithRelatedDataAsync(Id.Value);

        if (member == null)
            return null;

        var model = new MemberDetailsViewModel()
        {
            Email = member.Email,
            Gender = member.Gender.ToString(),
            Name = member.Name,
            Phone = member.Phone,
            Photo = member.Photo,
            Address = $"{member.Address?.BuildingNumber} - {member.Address?.Street} - {member.Address?.City}",
            DateOfBirth = member.DateOfBirth.ToString("d/MM/yyyy"),
            MemberShipStartDate = member.MemberShips.Where(x => x.IsActive).Select(x => x.CreatedAt.ToString("d/MM/yyyy")).FirstOrDefault(),
            MemberShipEndDate = member.MemberShips.Where(x => x.IsActive).Select(x => x.EndDate.ToString("d/MM/yyyy")).FirstOrDefault(),
            PlanName = member.MemberShips.Where(x => x.IsActive).Select(x => x.Plan.Name).FirstOrDefault()
        };
        return model;
    }
}

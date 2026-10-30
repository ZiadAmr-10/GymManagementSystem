using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;

namespace GymManagementSystem.BLL.Services.Members.Classes;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IHealthRecordRepository _healthRecordRepository;
    public MemberService(IMemberRepository membersRepository, IHealthRecordRepository healthRecordRepository)
    {
        _memberRepository = membersRepository;
        _healthRecordRepository = healthRecordRepository;
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
            id = x.Id,
            Name = x.Name,
            Email = x.Email,
            Phone = x.Phone,
            Gender = x.Gender.ToString(),
            Photo = x.Photo,
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

    public async Task<HealthRecordViewModel?> GetHealthRecordAsync(int? Id)
    {
        if (Id is null || Id <= 0)
            return null;

        var healthRecord = await _healthRecordRepository.GetHealthRecordAsync(Id);

        if (healthRecord is null)
            return null;

        return new HealthRecordViewModel()
        {
            Height = healthRecord.Height,
            Weight = healthRecord.Weight,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note,
        };

    }

    public async Task<MemberToUpdateViewModel?> GetMemberToUpdateAsync(int? Id)
    {
        if (Id is null || Id <= 0)
            return null;

        var member = await _memberRepository.GetByIdAsync(Id.Value);
        if (member is null)
            return null;

        var model = new MemberToUpdateViewModel()
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            BuildingNumber = member.Address?.BuildingNumber ?? 0,
            Street = member.Address?.Street ?? "Not Found",
            City = member.Address?.City ?? "Not Found",
        };

        return model;
    }

    public async Task<bool> UpdateMemberAsync(MemberToUpdateViewModel model)
    {
        try
        {
            if (model == null)
                return false;

            var member = await _memberRepository.GetByIdAsync(model.Id);

            if (member is null)
                return false;

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.Street = model.Street;
            member.Address.City = model.City;

            return await _memberRepository.UpdateAsync(member) > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}

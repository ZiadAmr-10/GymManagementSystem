using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using GymManagmentSystem.BLL.Common;

namespace GymManagementSystem.BLL.Services.Members.Classes;

public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;
    public MemberService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> AddMemberAsync(CreateMemberViewModel model)
    {
        try
        {
            var member = new Member()
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

            await _unitOfWork.memberRepository.AddAsync(member);
            var result = await _unitOfWork.SaveChangesAsync() > 0;
            if(result)
                  return Result.OK();
            return Result.Fail("Failed to add member");
        }
        catch
        {
            return Result.Fail("Internal Server error");
        }
   }
    

    public async Task<Result<IEnumerable<MemberViewModel>>> GetMembersAsync()
    {
        try
        {
            var members = await _unitOfWork.memberRepository.GetAllAsync();
            var mappedMembers = members.Select(x => new MemberViewModel()
            {
                id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Gender = x.Gender.ToString(),
                Photo = x.Photo,
            }).ToList(); // Forces immediate execution of the projection inside the try block.

            return Result<IEnumerable<MemberViewModel>>.OK(mappedMembers);
        }
        catch
        {
            return Result<IEnumerable<MemberViewModel>>.Fail("Internal Server Error");
        }
      
    }

    public async Task<MemberDetailsViewModel?> GetMemberAsync(int? Id)
    {
        if (Id == null || Id.Value == 0)
            return null;

        var member = await _unitOfWork.memberRepository.GetMemberWithRelatedDataAsync(Id.Value);

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

        var healthRecord = await _unitOfWork.healthRecordRepository.GetHealthRecordAsync(Id);

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

        var member = await _unitOfWork.memberRepository.GetByIdAsync(Id.Value);
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

            var member = await _unitOfWork.memberRepository.GetByIdAsync(model.Id);

            if (member is null)
                return false;

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.Street = model.Street;
            member.Address.City = model.City;

            await _unitOfWork.memberRepository.UpdateAsync(member) ;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteMemberAsync(int? Id)
    {
        try
        {
            if (Id is null || Id <= 0)
                return false;

            var member = await _unitOfWork.memberRepository.GetByIdAsync(Id.Value);
            if (member is null)
                return false;

            await _unitOfWork.memberRepository.DeleteAsync(member) ;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        catch
        {
            return false;
        }

    }
}

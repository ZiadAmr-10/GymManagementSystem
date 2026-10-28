using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace GymManagementSystem.PL.Controllers;

public class MembersController : Controller
{
    private readonly IMemberService _memberService;
    private readonly IValidator<CreateMemberViewModel> _validator;

    public MembersController(IMemberService memberService, IValidator<CreateMemberViewModel> validator)
    {
        _memberService = memberService;
        _validator = validator;
    }
    public async Task<IActionResult> Index()
    {
        var Members = await _memberService.GetMembersAsync();
        return View(Members);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberViewModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        if (!ModelState.IsValid)
            return View(nameof(Create),model);

        var result =await _memberService.AddMemberAsync(model);

        if (result)
        {
            TempData["SuccessMessage"] = "Member Created Successfuly";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = "Member Created Failed";

            return View(nameof(Create), model);
        }
    }
    public async Task<IActionResult> Details(int? id)
    {

        var member= await _memberService.GetMemberAsync(id);

        if (member == null)
        {
            TempData["ErrorMessage"] = "Member Not Found";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View(member);
        }


    }

    public async Task<IActionResult> HealthRecordDetails(int? id)
    {
        var model =await _memberService.GetHealthRecordAsync(id);
         if(model == null)
        {
            TempData["ErrorMessage"] = "Health Record Not Found";
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}

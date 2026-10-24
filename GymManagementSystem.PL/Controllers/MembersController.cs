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
            return RedirectToAction(nameof(Index));
        else
            return View(nameof(Create), model);
    }
}

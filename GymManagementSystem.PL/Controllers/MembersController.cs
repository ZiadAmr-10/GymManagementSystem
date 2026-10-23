using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.BLL.Services.Members.ViewModels;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymManagementSystem.PL.Controllers;

public class MembersController : Controller
{
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
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
        if (!ModelState.IsValid)  return View(model);

        var result =await _memberService.AddMemberAsync(model);

        if (result) 
            return RedirectToAction(nameof(Index));
        else
            return View(model);
    }
}

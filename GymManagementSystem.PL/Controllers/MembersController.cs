using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
}

using GymManagementSystem.BLL.Services.Plans.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementSystem.PL.Controllers;
public class PlanController : Controller
{
    private readonly IPlanService _planService;

    public PlanController(IPlanService planService)
    {
        _planService = planService;
    }

    public async Task<IActionResult> Index()
    {
        var plans = await _planService.GetPlansAsync();
        return View(plans);
    }

    //public async Task<IActionResult> Details(int id)
    //{
    //    var plan = await _planRepository.GetByIdAsync(id);
    //    if (plan == null)
    //        return RedirectToAction(nameof(Index));
    //    else
    //         return View(plan);
    //}
}


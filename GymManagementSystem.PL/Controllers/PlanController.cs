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

    public async Task<IActionResult> Details(int? id)
    {
        if(id == null || id <=0)
            return RedirectToAction(nameof(Index));

        var plan = await _planService.GetPlanDetailsAsync(id);
        if (plan == null)
            return RedirectToAction(nameof(Index));
        
        return View(plan);
    }
}


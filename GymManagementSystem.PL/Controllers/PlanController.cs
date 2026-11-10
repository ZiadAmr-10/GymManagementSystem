using FluentValidation;
using GymManagementSystem.BLL.Services.Plans.Interfaces;
using GymManagementSystem.BLL.Services.Plans.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementSystem.PL.Controllers;
public class PlanController : Controller
{
    private readonly IPlanService _planService;
    private readonly IValidator<PlanToUpdateViewModel> _validator;

    public PlanController(IPlanService planService,IValidator<PlanToUpdateViewModel> validator)
    {
        _planService = planService;
        _validator = validator;
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

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id <= 0)
            return RedirectToAction(nameof(Index));
        var plan = await _planService.GetPlanToUpdateAsync(id);
        if (plan == null)
            return RedirectToAction(nameof(Index));
        return View(plan);
    }
    [HttpPost]
    public async Task<IActionResult>Edit(PlanToUpdateViewModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        foreach(var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
         
        if (!ModelState.IsValid)
            return View(model);
        var result = await _planService.UpdatePlanAsync(model);

        if (result)
        {
            TempData["SuccessMessage"] = "Plan Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Failed to Update Plan";
        return View(model);
    } 
}


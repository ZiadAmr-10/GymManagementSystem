using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Repositories.Classes;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GymManagementSystem.PL.Controllers;
public class PlanController : Controller
{
    private IPlanRepository PlanRepository;

    public PlanController()
    {
        PlanRepository=new PlanRepository();
    }

    public async Task<IActionResult> Index()

    {
        var plan = await PlanRepository.GetALLAsync();
        return View(plan);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await PlanRepository.GetByIdAsync(id);
        if (plan == null)
            return RedirectToAction(nameof(Index));
        else
             return View(plan);
    }
}


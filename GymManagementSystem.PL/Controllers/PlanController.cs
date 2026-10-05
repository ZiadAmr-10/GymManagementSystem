using GymManagementSystem.DAL.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GymManagementSystem.PL.Controllers;
public class PlanController : Controller
{
    private readonly GymDbContext _context;
    public PlanController()
    {
        _context = new GymDbContext();
    }

    public async Task<IActionResult> Index()

    {
        var plan = await _context.Plans.ToListAsync();
        return View(plan);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await _context.Plans.FirstOrDefaultAsync(x => x.Id == id);
        return View(plan);
    }
}


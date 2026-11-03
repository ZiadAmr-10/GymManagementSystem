using GymManagementSystem.BLL.Services.Trainers.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.PL.Controllers;

public class TrainersController : Controller
{
    private readonly ITrainerService _trainerService;
    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }
    public async Task<IActionResult> Index()
    {
        var trainers = await _trainerService.GetTrainersAsync();

        return View(trainers);
    }
}

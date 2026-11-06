using FluentValidation;
using GymManagementSystem.BLL.Services.Trainers.Interface;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymManagementSystem.PL.Controllers;

public class TrainersController : Controller
{
    private readonly ITrainerService _trainerService;
    private readonly IValidator<CreateTrainerViewModel> _validator;
    public TrainersController(ITrainerService trainerService,IValidator<CreateTrainerViewModel> validator)
    {
        _trainerService = trainerService;
        _validator = validator;
    }
    public async Task<IActionResult> Index()
    {
        var trainers = await _trainerService.GetTrainersAsync();

        return View(trainers);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainerViewModel model )
    {
        var validationResult = await _validator.ValidateAsync(model);
        foreach(var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        if (!ModelState.IsValid)
            return View(nameof(Create), model);
        var result = await _trainerService.AddTrainerAsync(model);
        if (result)
        {
            TempData["SuccessMessage"] = "Trainer Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = "Trainer Created Failed";

            return View(nameof(Create), model);
        }
    }
}

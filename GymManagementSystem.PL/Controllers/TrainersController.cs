using FluentValidation;
using GymManagementSystem.BLL.Services.Trainers.Interface;
using GymManagementSystem.BLL.Services.Trainers.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementSystem.PL.Controllers;

public class TrainersController : Controller
{
    private readonly ITrainerService _trainerService;
    private readonly IValidator<CreateTrainerViewModel> _validator;
    private readonly IValidator<TrainerToUpdateViewModel> _updateValidator;

    public TrainersController(ITrainerService trainerService,IValidator<CreateTrainerViewModel> validator, IValidator<TrainerToUpdateViewModel> updateValidator)
    {
        _trainerService = trainerService;
        _validator = validator;
        _updateValidator = updateValidator;
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

    public async Task<IActionResult> Details(int? id)
    {
        if(id == null || id<=0)
            return RedirectToAction(nameof(Index));

        var trainer = await _trainerService.GetTrainerById(id.Value);
        if (trainer == null)
        {
            TempData["ErrorMessage"] = "Trainer Not Found";
            return RedirectToAction(nameof(Index));
        }
           
        return View(trainer);        
     }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id<=0)
            return RedirectToAction(nameof(Index));

        var trainer =await _trainerService.GetTrainerToUpdateAsync(id.Value);
        if (trainer == null)
        {
            TempData["ErrorMessage"] = "trainer Not Found";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View(trainer);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TrainerToUpdateViewModel model)
    {
        var validationResult = await _updateValidator.ValidateAsync(model);
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        if (!ModelState.IsValid)
            return View(nameof(Edit), model);

        var result= await _trainerService.UpdateTrainerAsync(model);

        if (result)
        {
            TempData["SuccessMessage"] = "Trainer Updated Successfuly";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to Update Trainer";
            return View(nameof(Edit), model);
        }
    } 
        
  }

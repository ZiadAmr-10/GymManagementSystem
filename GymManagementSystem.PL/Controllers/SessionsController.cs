using FluentValidation;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.PL.Controllers
{
    public class SessionsController: Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IValidator<CreateSessionViewModel> _validator;

        public SessionsController(ISessionService sessionService, IValidator<CreateSessionViewModel> validator)
        {
            _sessionService = sessionService;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _sessionService.GetAllSessionsAsync();
            return View(result.Value);
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {
            await LoadTrainerAndCategorisForSelectedList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadTrainerAndCategorisForSelectedList();
                return View(model);
            }

            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                await LoadTrainerAndCategorisForSelectedList();
                return View(model);
            }

            var result = await _sessionService.CreateSessionAsync(model);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await LoadTrainerAndCategorisForSelectedList();
                TempData["ErrorMessage"] = result.Error;
                return View(model);
            }

        
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _sessionService.GetSessionDetailsAsync(id);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = "Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(result.Value);
        }

        #region Helper Methods
        private async Task LoadTrainerAndCategorisForSelectedList()
         {
            var trainers = await _sessionService.GetTrainersForDropDownAsync();
            var categories = await _sessionService.GetCategoriesForDropDownAsync();

            ViewBag.Trainers = new SelectList(trainers.Value, "Id", "Name");
            ViewBag.Categories = new SelectList(categories.Value, "Id", "CategoryName");
        }
        #endregion
    }
}

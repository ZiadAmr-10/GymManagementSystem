using FluentValidation;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Models;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            var result = await _sessionService.GetSessionToUpdateAsync(id);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(Index));
            }

            await LoadTrainersForEditAsync(result.Value.TrainerId);

            return View(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SessionToUpdateViewModel? session)
        {
            if (session == null || session.Id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                await LoadTrainersForEditAsync(session.TrainerId);
                return View(session);
            }

            var result = await _sessionService.UpdateSessionAsync(session);

            if (!result.Success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.Error ?? "Failed to update session"
                );

                await LoadTrainersForEditAsync(session.TrainerId);

                return View(session);
            }

            TempData["SuccessMessage"] = "Session Updated Successfully";
            return RedirectToAction(nameof(Index));
        }



        #region Helper Methods
        private async Task LoadTrainerAndCategorisForSelectedList()
         {
            var trainers = await _sessionService.GetTrainersForDropDownAsync();
            var categories = await _sessionService.GetCategoriesForDropDownAsync();

            ViewBag.Trainers = new SelectList(trainers.Value, "Id", "Name");
            ViewBag.Categories = new SelectList(categories.Value, "Id", "CategoryName");
        }
        private async Task LoadTrainersForEditAsync(int? selectedTrainerId)
        {
            var trainers = await _sessionService.GetTrainersForDropDownAsync();

            ViewBag.Trainers = new SelectList(
                trainers.Value,
                "Id",
                "Name",
                selectedTrainerId
            );
        }
        #endregion
    }
}

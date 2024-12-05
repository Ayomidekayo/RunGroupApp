using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroupApp.Interface;
using RunGroupApp.Models;
using RunGroupApp.ViewModel;
using RunGroupApp.ViewModel.Dashboard;

namespace RunGroupApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private void mapUserEdit(AppUser User,EditUserDashboardVM editVm,ImageUploadResult photoResult)
        {
            User.Id = editVm.Id;
            User.Milage = editVm.Milage;
            User.Pace = editVm.Pace;
            User.State = editVm.State;
            User.City = editVm.City;
            User.ProfileImageUrl = photoResult.Url.ToString();
            
            
        }
        public DashboardController(IDashboardRepository dashboardRepository,
            IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService)
        {
            this._dashboardRepository = dashboardRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            var userClubs = await _dashboardRepository.GetAllClubsAysnc();
            var userRaces = await _dashboardRepository.GetAllRacesAysnc();

            var dashboardUser = new DashboardVM()
            {
                Races = userRaces,
                Clubs = userClubs,
            };
            return View(dashboardUser);
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var clubUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetById(clubUserId);
            if (user == null) return View("Error");
            var editUserDashboadVm = new EditUserDashboardVM()
            {
                Id = clubUserId,
                Milage = user.Milage,
                Pace = user.Pace,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State,
            };
            return View(editUserDashboadVm);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardVM editUserDashboardVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile",editUserDashboardVM);
            }
            var user = await _dashboardRepository.GetByIdAsNoTtracking(editUserDashboardVM.Id);
            if(user.ProfileImageUrl==""|| user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserDashboardVM.Image);
                //Optimistic Cocurrency =Tracking rerror
                //As no Tracking
                mapUserEdit(user,editUserDashboardVM, photoResult);
                _dashboardRepository.Update(user);
               return RedirectToAction("Index");
            }
            else
            {
                try
                {
                   await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Cloud not delete image");
                    return View(editUserDashboardVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editUserDashboardVM.Image);
                mapUserEdit(user, editUserDashboardVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}

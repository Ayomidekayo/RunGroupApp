using Microsoft.AspNetCore.Mvc;
using RunGroupApp.Interface;
using RunGroupApp.Models;
using RunGroupApp.ViewModel;

namespace RunGroupApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _repository;
        private readonly IPhotoService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClubController(IClubRepository repository,
            IPhotoService service,IHttpContextAccessor httpContextAccessor)
        {
            this._repository = repository;
            this._service = service;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task< IActionResult> Index()
        {
            IEnumerable<Club> clubs=await _repository.GetAllClubAsync();
            
            return View(clubs);
        }
        public async Task<IActionResult> Details(int id) 
        {
            Club club=await  _repository .GetClubAsync(id);
            return View(club);
        }
        
        public IActionResult Create()
        {
            var userClubId= _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubVM()
            {
                AppUserId = userClubId
            };
            return View(createClubViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubVM clubVM)
        {
            if (ModelState.IsValid)
            {
              var result=await _service.AddPhotoAsync(clubVM.Image);
                var club = new Club()
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId= clubVM.AppUserId,
                    Address = new Address()
                    {
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street,
                    }
                };
                 _repository.AddClub(club);
                return RedirectToAction("Index");
                
            }else
            {
                ModelState.AddModelError("", "Upload image failed");
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club=await _repository .GetClubAsync(id);
            if(club==null) return View("Error");
            var editClub = new EditClubVM()
            {
                Title = club.Title,
                Description = club.Description,
                Address = club.Address,
                AddressId = club.AddressId,
                RUL = club.Image,
                clubCategory = club.ClubCategory,
            };
            return View(editClub);
        }
        [HttpPost]
        public async Task< IActionResult> Edit(int id, EditClubVM editClubVM)
        {
          if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club model");
                return View("Edit",editClubVM);
            }
          var userClub=await _repository .GetClubAsyncAsNoTracking(id);
            if (userClub != null)
            {
                try
                {
                  await  _service.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception es)
                {

                    ModelState.AddModelError("", "Failed to delete photo");
                    return View( editClubVM);
                }
                var photoResult = await _service.AddPhotoAsync(editClubVM.Image);
                var club = new Club()
                {
                    Id = id,
                    Title = editClubVM.Title,
                    Description = editClubVM.Description,
                    Address = editClubVM.Address,
                    AddressId = editClubVM.AddressId,
                    Image=photoResult.Url.ToString()
                };
                _repository.UpdatClub(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editClubVM);
            }
              
        }
        public  async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _repository.GetClubAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clud = await _repository.GetClubAsync(id);
            if (clud == null) return View("Error");
            _repository.DeleteClub(clud);
            return RedirectToAction("Index");
        }
    }
}

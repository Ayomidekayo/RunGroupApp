using Microsoft.AspNetCore.Mvc;
using RunGroupApp.Data;
using RunGroupApp.Interface;
using RunGroupApp.Models;
using RunGroupApp.ViewModel;

namespace RunGroupApp.Controllers
{
    public class RaceController : Controller
    {
        
        private readonly IRaceRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _pservice;

        public RaceController(IRaceRepository repository,IHttpContextAccessor httpContextAccessor,
            IPhotoService pservice)
        {
            
            this._repository = repository;
            this._httpContextAccessor = httpContextAccessor;
            this._pservice = pservice;
        }
        public async Task< IActionResult> Index()
        {
            IEnumerable<Race> races=await _repository.GetAllRacesAsync();
            return View(races);
        } 
        public async Task< IActionResult> Detail(int id)
        {
            Race race = await _repository.GetRaceByIdAsync(id);
            return View(race);
        }
        public IActionResult Create()
        {
            var userRaceId=_httpContextAccessor.HttpContext.User.GetUserId();
            var CreateRaceVM = new CreateRaceVM()
            {
                AppUserId = userRaceId,
            };
            return View(CreateRaceVM);
        }
        [HttpPost]
        public async Task< IActionResult>Create(CreateRaceVM raceVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _pservice.AddPhotoAsync(raceVm.Image);
                var race = new Race()
                {
                    Title = raceVm.Title,
                    Description = raceVm.Description,
                    Image = result.Url.ToString(),
                    AppUserId=raceVm.AppUserId,
                    Address = new Address()
                    {
                        City = raceVm.Address.City,
                        State = raceVm.Address.State,
                        Street = raceVm.Address.Street,
                    }
                };
               _repository.Add(race);
                return RedirectToAction("Index");
            }else
            {
                ModelState.AddModelError("", "");
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _repository.GetRaceByIdAsync(id);
            if (race == null) return View("Error");
            var editRace = new EditRaceVm()
            {
                Title = race.Title,
                Description = race.Description,
                Address = race.Address,
                AddressId = race.AddressId,
                Url = race.Image,
                 RaceCategory= race.RaceCategory,
            };
            return View(editRace);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditRaceVm editRaceVm)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club model");
                return View("Edit", editRaceVm);
            }
            var userRace=await _repository.GetRaceByIdAsyncAsNoTracking(id);
            if (userRace != null)
            {
                try
                {
                  await  _pservice.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(editRaceVm);
                }
                var photoResult = await _pservice.AddPhotoAsync(editRaceVm.Image);
                var race = new Race()
                {
                    Id = id,
                    Title = editRaceVm.Title,
                    Description = editRaceVm.Description,
                    Address = editRaceVm.Address,
                    AddressId = editRaceVm.AddressId,
                    Image=photoResult.Url.ToString(),
                };
                _repository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editRaceVm);
            }
        }
        public async Task< IActionResult> Delete(int id)
        {
            var race=await _repository.GetRaceByIdAsync(id);
            if (race == null) return View("Error");
            return View(race);
        }

        [HttpPost,ActionName("Delete")]
        public async Task< IActionResult> DeleteRace(int id)
        {
            var race=await  _repository.GetRaceByIdAsync(id);
            if (race == null) return View("Error");
            _repository.Delete(race);
            return RedirectToAction("Index");
        }
    }
}

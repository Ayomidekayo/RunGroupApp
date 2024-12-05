using Microsoft.AspNetCore.Mvc;
using RunGroupApp.Interface;
using RunGroupApp.ViewModel.User;

namespace RunGroupApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task< IActionResult> Index()
        {
            var users =await _userRepository.GetAllUsers();
            List<UserVm> result = new List<UserVm>();
            foreach (var user in users)
            {
                var UserVm = new UserVm()
                {     
                    Id=user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Milage = user.Milage,
                };
                result.Add(UserVm);
            }
            return View(result);
        }
        public async Task< IActionResult> Detail(string id)
        {
            var user=await _userRepository.GetById(id);
            var userDetailsVm = new UserDetailsVm()
            {
                Id = user.Id,
                UserName=user.UserName,
                Milage = user.Milage,
                Pace = user.Pace,

            };
                 
            return View(userDetailsVm);
        }
    }
}

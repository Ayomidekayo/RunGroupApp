using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupApp.Helpers;
using RunGroupApp.Interface;
using RunGroupApp.Models;
using RunGroupApp.ViewModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunGroupApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger,IClubRepository clubRepository)
        {
            _logger = logger;
            this._clubRepository = clubRepository;
        }

        public async Task< IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeVm = new HomeVM();
            try
            {
                string url = "Https://ipinfo.io?token=1de636ef7d688f\r\n";
                var info=new WebClient().DownloadString(url);
                ipInfo =JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRII = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRII.EnglishName;
                homeVm.City=ipInfo.City;
                homeVm.State = ipInfo.Region;
                if(ipInfo.Region != null)
                {
                    homeVm.Clubs=await _clubRepository.GetClubByCityAsync(homeVm.City);
                }else
                {
                    homeVm.Clubs = null;
                }
                return View(homeVm);
            }
            catch (Exception)
            {

                homeVm.Clubs=null;
            }
            return View(homeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

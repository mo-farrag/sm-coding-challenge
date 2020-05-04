using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using sm_coding_challenge.Models;
using sm_coding_challenge.Services.PlayerService;
using sm_coding_challenge.Services.HostedServices;

namespace sm_coding_challenge.Controllers
{
    public class HomeController : Controller
    {

        private IPlayerService _playerService;
        public HomeController(IPlayerService playerService)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PlayerAsync(string id)
        {
            return Json(await Task.Run(() => _playerService.GetPlayerById(id)));
        }

        [HttpGet]
        public async Task<IActionResult> PlayersAsync(string ids, string id = "")
        {
            // I added another id optional parameter to fix the issue of one of the links in the home page that uses 'id' instead of 'ids'
            var inputIds = string.IsNullOrEmpty(ids) ? id : ids;
            var idList = inputIds.Split(',').Distinct().ToArray();
            var returnList = new List<PlayerModel>();

            foreach (var i in idList)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    var player = await Task.Run(() => _playerService.GetPlayerById(i));
                    if (player != null)
                        returnList.Add(player);
                }
            }
            return Json(returnList);
        }

        [HttpGet]
        public async Task<IActionResult> LatestPlayersAsync(string ids)
        {
            var idList = ids.Split(',').Distinct().ToArray();
            return Json(await Task.Run(() => _playerService.GetLatestPlayerById(idList)));
        }

        [HttpGet]
        public async Task<IActionResult> ManualRefresh()
        {
            return Json(await Task.Run(() => _playerService.RefreshData()));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

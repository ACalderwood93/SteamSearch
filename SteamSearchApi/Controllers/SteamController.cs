using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Fluxter.SteamWebAPI;
using Fluxter.SteamWebAPI.Fluent.General.ISteamUser;
using Fluxter.SteamWebAPI.Interfaces.General.ISteamUser.GetFriendList;
using SteamSearchApi.Models;
using SteamSearchApi.Models.Interfaces;
using SteamSearchApi.Models.Repositories;
using SteamSearchApi.Models.Responses;

namespace SteamSearchApi.Controllers
{
    public class SteamController : ApiController
    {

        [Route("api/steam/getuser/{steamId}")]
        public IHttpActionResult GetUser(string steamId)
        {

            var ip = HttpContext.Current.Request.UserHostAddress;
            Player player = new SteamRepository().GetUser(steamId, ip);
            return Ok(player);
        }

        [Route("api/steam/getrecentgames/{steamId}")]
        public IHttpActionResult GetRecentGames(string steamId)
        {

            var req = SteamWebAPI.CustomRequest("IPlayerService", "GetRecentlyPlayedGames", "v0001", new { steamid = steamId });

            var responseString = req.GetResponseString(RequestFormat.JSON);


            return Ok(responseString);


        }

        [Route("api/steam/getownedgames/{steamId}")]
        public IHttpActionResult GetOwnedGames(string steamId)
        {

            // var req = SteamWebAPI.CustomRequest("IPlayerService", "GetOwnedGames", "v0001", new { steamid = steamId, include_appinfo = 1, include_played_free_games = 1 });
            // var responseString = req.GetResponseString(RequestFormat.JSON);

            var repo = new SteamRepository();
            var result = repo.GetOwnedGames(steamId);


            return Ok(result);


        }

        [Route("api/steam/getallgames")]
        public IHttpActionResult GetAllGames()
        {

            var req = SteamWebAPI.CustomRequest("ISteamApps", "GetAppList", "v2", new { });

            var responseString = req.GetResponseString(RequestFormat.JSON);


            return Ok(responseString);
        }

        [Route("api/steam/getFriends/{steamId}")]
        public IHttpActionResult GetFriends(string steamId)
        {

            var repo = new SteamRepository();
            var friends = repo.GetFriends(steamId);

            return Ok(friends);
        }

        [Route("api/steam/gettopgames")]
        public IHttpActionResult GetTopGamesAsync()
        {

            var repo = new SteamRepository();
            string sTopGames = repo.GetTopGames();

            return Ok(sTopGames);

        }


        [Route("api/steam/getGamesInCommon")]
        [HttpPost]
        public IHttpActionResult GetGamesInCommon([FromBody] List<Player> data)
        {
            var repo = new SteamRepository();
            var result = repo.GetGamesInCommon(data);
            return Ok(result);

        }


        [Route("api/steam/getallcatagories")]
        [HttpGet]
        public IHttpActionResult GetAllCatagories()
        {
            var repo = new MongoRepository<Catagory>();
            var result = repo.GetAll();
            return Ok(result);
            return Ok();
        }

    }
}
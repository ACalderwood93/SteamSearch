using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Fluxter.SteamWebAPI;


namespace SteamSearchApi.Controllers
{
    public class SteamController : ApiController
    {

        [Route("api/steam/getuser/{steamId}")]
        public IHttpActionResult GetUser(string steamId)
        {
            var req = SteamWebAPI.CustomRequest("ISteamUser", "GetPlayerSummaries", "v0002", new { steamids = steamId });

            var responseString = req.GetResponseString(RequestFormat.JSON);


            return Ok(responseString);
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

            var req = SteamWebAPI.CustomRequest("IPlayerService", "GetOwnedGames", "v0001", new { steamid = steamId, include_appinfo = 1, include_played_free_games = 1 });

            var responseString = req.GetResponseString(RequestFormat.JSON);


            return Ok(responseString);


        }

        [Route("api/steam/getallgames")]
        public IHttpActionResult GetAllGames()
        {

            var req = SteamWebAPI.CustomRequest("ISteamApps", "GetAppList", "v2", new { });

            var responseString = req.GetResponseString(RequestFormat.JSON);


            return Ok(responseString);
        }

    }
}
using Fluxter.SteamWebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models.Repositories
{
    public class SteamRepository
    {

       public string GetUser (string steamId)
        {

            var req = SteamWebAPI.CustomRequest("ISteamUser", "GetPlayerSummaries", "v0002", new { steamids = steamId });

            var responseString = req.GetResponseString(RequestFormat.JSON);


            return responseString;
        }

        public string GetRecentGames(string steamId)
        {


            return "";
        }

        public string GetOwnedGames(string steamId) {


            return "";
        }

        public string GetFriends(string steamId)
        {

            return "";
        }


        public string GetGamesInCommon(string steamId,string query)
        {


            return "";
        }



    }
}
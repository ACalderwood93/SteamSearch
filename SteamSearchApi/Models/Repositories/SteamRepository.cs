using Fluxter.SteamWebAPI;
using Fluxter.SteamWebAPI.Fluent;
using SteamSearchApi.Helpers;
using SteamSearchApi.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using System.Configuration;


namespace SteamSearchApi.Models.Repositories
{
    public class SteamRepository
    {

        public Player GetUser(string steamId, string ip)
        {
            var repo = new MongoRepository<UserQuery>();

            Player dbPlayer = null;
            if (!Util.isValidSteamId(steamId)) // before we send this SteamID up in the request we check if its even in a valid format
            {

                dbPlayer = repo.Get<Player>(x => x.Personaname.ToLower() == steamId.ToLower()).FirstOrDefault(); // do we have a record of that in our DB

                if (dbPlayer != null)
                    steamId = dbPlayer.Steamid; // Found it.
                else
                    steamId = GetSteamId(steamId); // no Go Request it from Steam



            }


            var req = SteamWebAPI.CustomRequest("ISteamUser", "GetPlayerSummaries", "v0002", new { steamids = steamId });
            var response = _HandleResponse<HttpUserResponse>(req);



            var query = new UserQuery()
            {
                SteamId = steamId,
                IpAddress = ip,
                DateCreated = DateTime.Now
            };

            repo.Insert(query);

            var player = response.Response.Players.First(); // get the player object from Steam

            if (!repo.Exists<Player>(x => x.Steamid == steamId))
            { // do we already have a record in the DB for this user
                repo.Insert(player); // Nope, insert them
            }
            else
            {

                dbPlayer = dbPlayer ?? repo.Get<Player>(x => x.Steamid == steamId).FirstOrDefault(); // get the Player from the DB. 

                // has the user changed their display name
                if (player.Personaname != dbPlayer.Personaname)
                {
                    dbPlayer.Personaname = player.Personaname;
                    repo.Update(dbPlayer);
                }

            }



            // have we got this player Already


            return player;
        }
        public List<Player> GetUsers(string[] steamIds)
        {

            var sSteamIds = "";
            var t = steamIds.ToString();

            foreach (var id in steamIds)
            {
                sSteamIds += id + ",";
            }
            var req = SteamWebAPI.CustomRequest("ISteamUser", "GetPlayerSummaries", "v0002", new { steamids = sSteamIds });
            var response = _HandleResponse<HttpUserResponse>(req);
            return response.Response.Players.ToList();
        }

        public List<Game> GetRecentGames(string steamId)
        {

            var req = SteamWebAPI.CustomRequest("IPlayerService", "GetRecentlyPlayedGames", "v0001", new { steamid = steamId });

            var response = _HandleResponse<HttpRecentlyPlayedGames>(req);
            return response.Response.Games;
        }
        public List<Game> GetOwnedGames(string steamId)
        {

            var req = SteamWebAPI.CustomRequest("IPlayerService", "GetOwnedGames", "v0001", new { steamid = steamId, include_appinfo = 1, include_played_free_games = 1 });
            var response = _HandleResponse<HttpGetOwnedGamesResponse>(req);
            return response.Response.Games;



        }
        public List<Player> GetFriends(string steamId)
        {

            var req = SteamWebAPI.CustomRequest("ISteamuser", "GetFriendList", "v0001", new { steamId = steamId, relationship = "friend" });
            var response = _HandleResponse<HttpFriendListResponse>(req);


            var steamIds = response.Friendslist.Friends.Select(x => x.Steamid).ToArray();
            var users = GetUsers(steamIds);

            return users;
        }
        public IEnumerable<long> GetGamesInCommon(List<Player> players)
        {
            IEnumerable<long> matchedAppIds = null;
            var repo = new MongoRepository<Player>();
            foreach (var player in players)
            {



                player.OwnedGames = this.GetOwnedGames(player.Steamid);

                if (player.OwnedGames == null)
                    continue;

                var appIds = player.OwnedGames.Select(x => x.Appid);
                if (matchedAppIds == null)
                {
                    matchedAppIds = appIds;
                }


                matchedAppIds = matchedAppIds.Intersect(appIds).ToList();




            }

            return matchedAppIds;
        }
        public string GetSteamId(string username)
        {

            var req = SteamWebAPI.CustomRequest("ISteamUser", "ResolveVanityURL", "v0001", new { vanityurl = username });

            var result = _HandleResponse<HttpGetVanityUrlResponse>(req);

            if (result == null || result.Response.SteamId == null)
                throw new Exception("Unable to convert to Valid SteamID");


            return result.Response.SteamId;

        }


        private T _HandleResponse<T>(SteamCustomBuilder request)
        {
            var sResponse = request.GetResponseString(RequestFormat.JSON);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(sResponse);
        }


    }
}
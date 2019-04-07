using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SteamSearchApi.Helpers
{
    public static class Util
    {

        public static DateTime ConvertFromUnixTimeStamp(long timeStamp)
        {

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();
            return dtDateTime;

        }
        public static bool isValidSteamId(string query)
        {
            var regexExp = new Regex(@"[0 - 9]{ 17}");
            return regexExp.Match(query).Success;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task006.Models;

namespace Task006
{
    class Wrapper
    {
        private const string baseUrl = @"https://api.guildwars2.com/v2";

        private const string accountAdo = @"/account";
        private const string WorldAdo = @"/worlds";
        private const string GuildAdo = @"/guild/";
        private const string CharactersAdo = @"/characters";
        private const string WalletAdo = @"/wallet";
        private const string CurrencyAdo = @"/currencies";

        public static async Task<AccountInfoModel> GetAccInfo(string accessToken)
        {
            var url = $"{baseUrl}{accountAdo}?access_token={accessToken}";
            var response = await Request.GetAsync(url);
            return JsonConvert.DeserializeObject<AccountInfoModel>(response);
        }

        public static async Task<List<WorldInfoModel>> GetWorldInfo (int worldId)
        {
            var urlWorld = $"{baseUrl}{WorldAdo}?ids={worldId}";
            var responseWorld = await Request.GetAsync(urlWorld);
            return JsonConvert.DeserializeObject<List<WorldInfoModel>>(responseWorld);
        }

        public static async Task<GuildInfoModel> GetGuildInfo(string guildId)
        {
            try
            {
                var urlGuild = $"{baseUrl}{GuildAdo}{guildId}";
                var responseGuild = await Request.GetAsync(urlGuild);

                return JsonConvert.DeserializeObject<GuildInfoModel>(responseGuild);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<List<GuildInfoModel>> GetGuildInfo(List<string> ids)
        {
            try
            {
                var guildList = new List<GuildInfoModel>();
                foreach (var guildId in ids)
                {
                    guildList.Add(await GetGuildInfo(guildId));
                }

                return guildList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

            
}
}

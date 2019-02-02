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
        
        public static async Task<List<string>> GetCharInfo(string accessToken)
        {
            var urlCharacter = $"{baseUrl}{CharactersAdo}?access_token={accessToken}";
            var responseCharacter = await Request.GetAsync(urlCharacter);
            return JsonConvert.DeserializeObject<List<string>>(responseCharacter);
        }

        public static async Task<CharactersCoreModel> GetCharacterInfo(string nameEncoded, string accessToken)
        {
            var urlCharacterName = $"{baseUrl}{CharactersAdo}/{nameEncoded}/core?access_token={accessToken}";
            var responseCharacterName = await Request.GetAsync(urlCharacterName);
            return JsonConvert.DeserializeObject<CharactersCoreModel>(responseCharacterName);
        }

        public static async Task<TitleInfoModel> GetTitleInfo(int titleId)
        {
            var urlTitleInfo = $"{baseUrl}/titles/{titleId}";
            var responseTitleInfo = await Request.GetAsync(urlTitleInfo);
            return JsonConvert.DeserializeObject<TitleInfoModel>(responseTitleInfo);
        }

        public static async Task<List<CurrencyModelShort>> GetWalletInfo(string accessToken)
        {
            var urlWalletInfo = $"{baseUrl}{accountAdo}{WalletAdo}?access_token={accessToken}";
            var responseWallet = await Request.GetAsync(urlWalletInfo);
            return JsonConvert.DeserializeObject<List<CurrencyModelShort>>(responseWallet);
        }

        public static async Task<List<CurrencyModelFull>> GetCurrencyInfo(List<int> currencyList)
        {
            var urlCurrencyInfo = $"{baseUrl}{CurrencyAdo}?ids={string.Join(",", currencyList)}";
            var responseCurrency = await Request.GetAsync(urlCurrencyInfo);
            return JsonConvert.DeserializeObject<List<CurrencyModelFull>>(responseCurrency);
        }

    }
}

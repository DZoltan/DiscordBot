using RiotSharp;
using RiotSharp.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.LoL
{
    class LoLData
    {
        string apikey { get; set; }
         
        public LoLData(string key)
        {
            apikey = key;
        }
        public string Search_Summoner(string summoner_name)
        {
            
            try
            {
                var api = RiotApi.GetDevelopmentInstance(apikey);

                var summoner = api.Summoner.GetSummonerByNameAsync(Region.Eune, summoner_name).Result;
                var name = summoner.Name;
                var level = summoner.Level;
                var accountId = summoner.AccountId;

                return "A " + name + " nevű játékos " + level + " szintű!";
            }
            catch (RiotSharpException ex)
            {
                return "Valami hiba történt!" + ex;
            }
        } 
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ClashOfClans;
using ClashOfClans.Models;
using ClashOfClans.Search;
using System.Threading.Tasks;

namespace DiscordBot.CoC
{
    class CoCData
    {
        ClashOfClansClient coc = new ClashOfClansClient();

        public CoCData(string key)
        {
            ClashOfClansClient coc_key = new ClashOfClansClient(key);
            this.coc = coc_key;
        }

        
        
        public async Task<string> Search_Player(string name)
        {
            var player = await coc.Players.GetPlayerAsync(name);
            string msg =  "A " + player.Name + " nevű játékosnak " + player.Trophies + " trófeája van.";
            Console.WriteLine( msg);
            return msg;
        }

        public async Task<string> Search_Clan(string clan_name)
        {
            var clan = await coc.Clans.GetClanAsync(clan_name);
            string msg = "A " + clan.Name + " nevű klán " + clan.ClanLevel + " -os szintű és " + clan.Members + " tagja van.";
            Console.WriteLine(msg);
            return msg;
        }
    }
}

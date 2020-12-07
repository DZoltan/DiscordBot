using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using DiscordBot.CoC;
using DiscordBot.LoL;
using System.Globalization;

namespace DiscordBot
{
    class Program
    {
		
		

		string discord_key;
		string coc_key;
		string lol_key;
		public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

		public void GetKeys()
        {
			string[] keys = System.IO.File.ReadAllLines(@"C:\Users\septe\Desktop\api_keys_dc.txt");
			discord_key = keys[0];
			coc_key = keys[1];
			lol_key = keys[2];
        }


		public async Task MainAsync()
		{
			GetKeys();
			_client = new DiscordSocketClient();

			_client.Log += Log;
			_client.MessageReceived += MessageReceived;

			await _client.LoginAsync(TokenType.Bot,
				discord_key);
			await _client.StartAsync();

			await Task.Delay(-1);
		}

		private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

		private async Task MessageReceived(SocketMessage message)
		{
			string[] messages = message.Content.Split(' ');


			if (message.Content == "!hello")
			{
				await message.Channel.SendMessageAsync("Szia, WoWBot vagyok, a Sons of Titans Guild botja!");
			}

			else if (messages[0] == "!hello")
            {
				await message.Channel.SendMessageAsync("Szia, " + messages[1]);
			}
			
			else if (message.Content == "!help"){
				await message.Channel.SendMessageAsync("A következő parancsok érhetőek el: !hello , !hello + név, !help");
			}

			else if(messages[0] == "!coc")
            {
				CoCData coc = new CoCData(coc_key);
				if (messages[1] == "player")
                {
					
					string msg = await coc.Search_Player(messages[2]);

					await message.Channel.SendMessageAsync(msg);

				}

				else if (messages[1] == "clan")
                {
					string msg = await coc.Search_Clan(messages[2]);

					await message.Channel.SendMessageAsync(msg);
				}

				else
                {
					await message.Channel.SendMessageAsync("A következő parancsok érhetőek el a coc parancshoz:");
				}
            }

			else if(messages[0] == "!lol")
            {
				LoLData lol = new LoLData(lol_key);
				if (messages[1] == "summoner")
                {
					await message.Channel.SendMessageAsync(lol.Search_Summoner(messages[2]));
				}
            }

			else
            {
				if(message.Content[0] == '!')
                {
					await message.Channel.SendMessageAsync("Sajnálom, nincs ilyen parancs!");
				}
            }
		}
	}
}

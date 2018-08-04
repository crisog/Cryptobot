using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using static Cryptobot.Modules.Cryptommands;
using System.Collections.Generic;
using System.Threading;

namespace Cryptobot
{
    public class Program
    {
        //public static List<Alert> runningAlerts;
        static void Main(string[] args)
        {
            /*runningAlerts = new List<Alert>();
            new Thread(() =>
            {
                while (true)
                {
                    List<Alert> toRemove = new List<Alert>();
                    foreach (Alert single in runningAlerts)
                    {
                        double result = single.Check();
                        // Console.WriteLine("{0} : {1} / {2}", single.Channel.Id, result, single.AlertThreshold);
                        if (result > 0d)
                        {
                            var s = string.Format("{0:0.00}", result);

                            single.Channel.SendMessageAsync(@"
```http
 " + "***" + single.Currency + " has just reached: $" + s + " * **" +
 "```");
                            toRemove.Add(single);
                        }
                    }
                    foreach (Alert single in toRemove)
                    {
                        runningAlerts.Remove(single);
                    }
                    Thread.Sleep(10 * 1000);
                }
            }).Start();
            /* Service serv = new Service(1, 3600, () => {
                 Console.WriteLine("Schedule Message Launched.");
             });
         */
            try
            {
                new Program().StartAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        //=> new Program().StartAsync().GetAwaiter().GetResult();



        private DiscordSocketClient _client;

        private CommandHandler _handler;



        public async Task StartAsync()
        {
            try
            {
                _client = new DiscordSocketClient();

            _client.GuildAvailable += GuildAvailableHandler;
            
                await _client.LoginAsync(TokenType.Bot, "MzYzMDczOTgyMDkxNDkzMzc3.DK77Mg.hFe4UqfascQ9BkkVuuUhRD3bYdM");
                await _client.StartAsync();
                Console.ForegroundColor = ConsoleColor.Green;
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR FOUND: Your bot token may be wrong." + Environment.NewLine + "Contact my developer if it doesn't get fixed");

            }

            _handler = new CommandHandler();

            await _handler.InitializeAsync(_client);

            await Task.Delay(-1);
        }

        private Task GuildAvailableHandler(SocketGuild arg)
        {
            try
            {
                Console.WriteLine(@"
Cryptobot v2.0 by Admirably Connected - using Coinmarketcap API
");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Task.CompletedTask;
            }
        }
    }

}
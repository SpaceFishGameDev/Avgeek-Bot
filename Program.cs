using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;

namespace AvgeekBot
{
    class Program
    {

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = "ODMxNjIzOTQxMTk3MDA0ODYw.YHX8Jg.yh1_9loDHwa3_paXTa-HMnmHWzE";

            _client.Log += _client_Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }

        }
    }


    public class Module : ModuleBase<SocketCommandContext>
    {
        [Command("Bootcamp")]
        public async Task Help()
        {
            await ReplyAsync("`Welcome, this bot was built by Space Fish. Now this is the bootcamp for this bot so listen up`"
                + Environment.NewLine + "`Number one you got your !schedule <day> this shows Avgeek's schedule , now for Avgeek since this only works for his role, do !addsched <day> <time>`");
        }
        [Command("addsched")]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task addsched( [Remainder] string RE)
        {
            string save = "";

            if(RE.Contains("Day1"))
            {
                save = "Monday : ";
                save += RE.Replace("Day1","");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Mon.txt"))
                {
                    SW.Write(save);
                }
            }
            else if (RE.Contains("Day2"))
            {
                save = "Tuesday : ";
                save += RE.Replace("Day2", "");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Tue.txt"))
                {
                    SW.Write(save);
                }
            }
            else if (RE.Contains("Day3"))
            {
                save = "Wednesday : ";
                save += RE.Replace("Day3", "");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Wednesday.txt"))
                {
                    SW.Write(save);
                }
            }
            else if (RE.Contains("Day4"))
            {
                save = "Thursday : ";
                save += RE.Replace("Day4", "");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Thursday.txt"))
                {
                    SW.Write(save);
                }
            }
            else if (RE.Contains("Day5"))
            {
                save = "Friday : ";
                save += RE.Replace("Day5", "");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Friday.txt"))
                {
                    SW.Write(save);
                }
            }
            else if (RE.Contains("Day6"))
            {
                save = "Saturday : ";
                save += RE.Replace("Day6", "");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Saturday.txt"))
                {
                    SW.Write(save);
                }
            }
            else if (RE.Contains("Day7"))
            {
                save = "Sunday : ";
                save += RE.Replace("Day7", "");
                await ReplyAsync("Schedule updated. " + save);
                using (StreamWriter SW = new StreamWriter(@"C:\DiscordBot\Sunday.txt"))
                {
                    SW.Write(save);
                }
            }
            else
            {
                await ReplyAsync("Umm, " + RE + " isnt a valid day");
            }

          

        }
        [Command("schedule")] 
        public async Task sched ([Remainder] string txt) 
        {
            if(txt.Contains("monday"))
            {
                var dir = @"C:\Discordbot\mon.txt";
              string file = File.ReadAllText(dir);
                await ReplyAsync(file);

            }
            else if (txt.Contains("tuesday"))
            {
                var dir = @"C:\Discordbot\tue.txt";
                string file = File.ReadAllText(dir);
                await ReplyAsync(file);


            }
            else if (txt.Contains("wednesday"))
            {
                var dir = @"C:\Discordbot\wednesday.txt";
                string file = File.ReadAllText(dir);
                await ReplyAsync(file);


            }
            else if (txt.Contains("thursday"))
            {
                var dir = @"C:\Discordbot\thursday.txt";
                string file = File.ReadAllText(dir);
                await ReplyAsync(file);


            }
            else if (txt.Contains("friday"))
            {
                var dir = @"C:\Discordbot\friday.txt";
                string file = File.ReadAllText(dir);
                await ReplyAsync(file);


            }
            else if (txt.Contains("saturday"))
            {
                var dir = @"C:\Discordbot\wednesday.txt";
                string file = File.ReadAllText(dir);
                await ReplyAsync(file);


            }
            else if (txt.Contains("sunday"))
            {
                var dir = @"C:\Discordbot\wednesday.txt";
                string file = File.ReadAllText(dir);
                await ReplyAsync(file);

            }
            Console.WriteLine("Schedule Command Used");
        }
        

    } 
}

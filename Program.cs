using DSharpPlus;

namespace Studow;

class Program
{
    static async Task Main(string[] args)
    {
        var postgres = await Services.PostgresService.Start();

        var discord = new DiscordClient(new DiscordConfiguration()
        {
            Token = Environment.GetEnvironmentVariable("BotToken"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        });

        discord.MessageCreated += async (s, e) => await Handlers.MessageEventsHandler.OnMessageCreated(s, e, postgres);

        await discord.ConnectAsync();
        await Task.Delay(-1);
    }
}


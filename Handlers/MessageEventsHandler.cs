using DSharpPlus;
using DSharpPlus.EventArgs;
using Npgsql;

namespace Studow.Handlers;

internal class MessageEventsHandler
{
    public static async Task OnMessageCreated(DiscordClient client, MessageCreateEventArgs args, NpgsqlDataSource postgrres)
    {
        // TODO
    }
}

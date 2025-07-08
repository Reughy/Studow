using Microsoft.Extensions.Logging;
using Npgsql;

namespace Studow.Services;

internal class PostgresService
{
    private record Credentials(string Address, string User, string Password);

    private static Credentials GetCredentials()
    {
        var address = Environment.GetEnvironmentVariable("PGAddress");
        var user = Environment.GetEnvironmentVariable("PGUser");
        var password = Environment.GetEnvironmentVariable("PGPassword");

        if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException("One of the environment variables required for starting a Postgres connection is either null or missing!\nCheck your environment variables and try again.");

        return new Credentials(address, user, password);
    }

    public static async Task<NpgsqlDataSource> Start()
    {
        // TODO: Add logging for connection attempts and errors
        var creds = GetCredentials();
        var dataSourceBuilder = new NpgsqlDataSourceBuilder($"Host={creds.Address};Username={creds.User};Password={creds.Password}");

        var dataSource = dataSourceBuilder.Build();
        return dataSource;
    }
}

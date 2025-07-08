
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

# Here we store all the credentials needed by the bot.
# Note: do not change any names of the following variables, as it can results in the bot malfunctioning.
# Note: Remove all the values from the environment variables before pushing to a public repository.
ENV BotToken=""

# Note: PG stands for PostgreSQL.
ENV PGAddress=""
ENV PGUser=""
ENV PGPassword=""

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Studow.csproj", "."]
RUN dotnet restore "./Studow.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Studow.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Studow.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Studow.dll"]
# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY DnDBot.Application/*.csproj ./DnDBot.Application/
COPY DnDBot.Bot/*.csproj ./DnDBot.Bot/
COPY DnDBot.Domain/*.csproj ./DnDBot.Domain/
COPY DnDBot.Infrastructure/*.csproj ./DnDBot.Infrastructure/
COPY DnDBot.Shared/*.csproj ./DnDBot.Shared/

RUN dotnet restore

COPY . ./

RUN dotnet publish DnDBot.Bot/DnDBot.Bot.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DnDBot.Bot.dll"]

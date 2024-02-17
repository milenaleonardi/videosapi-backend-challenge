# Define a imagem base. Aqui estamos utilizando a imagem oficial do SDK do .NET Core como base.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#HTTP
EXPOSE 8080
#HTTPS
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VideosAluraAPI/VideosAluraAPI.csproj", "VideosAluraAPI/"]
RUN dotnet restore "./VideosAluraAPI/./VideosAluraAPI.csproj"
COPY . .
WORKDIR "/src/VideosAluraAPI"
RUN dotnet build "./VideosAluraAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VideosAluraAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideosAluraAPI.dll"]

# docker build -t milenaleonardi/videosaluraapi .

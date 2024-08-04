FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/TariffComparison.Domain/TariffComparison.Domain.csproj", "src/TariffComparison.Domain/"]
COPY ["src/TariffComparison.Application/TariffComparison.Application.csproj", "src/TariffComparison.Application/"]
COPY ["src/TariffComparison.Infrastructure/TariffComparison.Infrastructure.csproj", "src/TariffComparison.Infrastructure/"]
COPY ["src/TariffComparison.API/TariffComparison.API.csproj", "src/TariffComparison.API/"]

RUN dotnet restore "src/TariffComparison.API/TariffComparison.API.csproj"

COPY . .
WORKDIR "/src/src/TariffComparison.API"
RUN dotnet build "TariffComparison.API.csproj" -c Release -o /app/build -r linux-x64

FROM build AS publish
RUN dotnet publish "TariffComparison.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID 
ENTRYPOINT ["dotnet", "TariffComparison.API.dll", "urls=http://0.0.0.0:8080"]
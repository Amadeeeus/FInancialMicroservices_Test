FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Background.CurrencyRate/Background.CurrencyRate.csproj", "Background.CurrencyRate/"]
RUN dotnet restore "Background.CurrencyRate/Background.CurrencyRate.csproj"
COPY . .
WORKDIR "/src/Background.CurrencyRate"
RUN dotnet build "./Background.CurrencyRate.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Background.CurrencyRate.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Background.CurrencyRate.dll"]

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserService.Tests/UserService.Tests.csproj", "UserService.Tests/"]
COPY ["UserService.Api/UserService.Api.csproj", "UserService.Api/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Application/UserService.Application.csproj", "UserService.Application/"]
RUN dotnet restore "UserService.Tests/UserService.Tests.csproj"
COPY . .
WORKDIR "/src/UserService.Tests"
RUN dotnet build "./UserService.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Tests.dll"]

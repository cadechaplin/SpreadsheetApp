FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HW5ConsoleApp/HW5ConsoleApp.csproj", "HW5ConsoleApp/"]
RUN dotnet restore "HW5ConsoleApp/HW5ConsoleApp.csproj"
COPY . .
WORKDIR "/src/HW5ConsoleApp"
RUN dotnet build "HW5ConsoleApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HW5ConsoleApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HW5ConsoleApp.dll"]

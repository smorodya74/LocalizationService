# 1. Базовый рантайм-образ — сюда мы будем копировать и запускать наше приложение
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5172

# 2. Образ SDK для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LocalizationService.WebAPI/LocalizationService.WebAPI.csproj", "LocalizationService.WebAPI/"]
RUN dotnet restore "./LocalizationService.WebAPI/LocalizationService.WebAPI.csproj"
COPY . .
WORKDIR "/src/LocalizationService.WebAPI"
RUN dotnet build "./LocalizationService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Публикация (производится из результата сборки)
FROM build AS publish
RUN dotnet publish "./LocalizationService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Финальный образ — берём рантайм, копируем туда опубликованное приложение и задаём команду запуска
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocalizationService.WebAPI.dll"]

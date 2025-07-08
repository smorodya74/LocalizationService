FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5172

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY . /app/. 
RUN dotnet restore LocalizationService.WebAPI/LocalizationService.WebAPI.csproj
RUN dotnet publish LocalizationService.WebAPI/LocalizationService.WebAPI.csproj \
    -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LocalizationService.WebAPI.dll"]

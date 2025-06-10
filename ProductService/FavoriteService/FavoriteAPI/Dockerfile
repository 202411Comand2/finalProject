# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProductService/FavoriteService/FavoriteAPI/FavoriteAPI.csproj", "ProductService/FavoriteService/FavoriteAPI/"]
COPY ["ProductService/SupperDto/SupperBackEndDto/SupperBackEnd.Dto.csproj", "ProductService/SupperDto/SupperBackEndDto/"]
COPY ["ProductService/FavoriteService/BLL/FavoriteService.BLL.csproj", "ProductService/FavoriteService/BLL/"]
COPY ["ProductService/FavoriteService/DAL/FavoriteService.DAL.csproj", "ProductService/FavoriteService/DAL/"]
COPY ["ProductService/FavoriteService/Domain/FavoriteService.Domain.csproj", "ProductService/FavoriteService/Domain/"]
COPY ["DAL/Platform.DAL.csproj", "DAL/"]
RUN dotnet restore "./ProductService/FavoriteService/FavoriteAPI/FavoriteAPI.csproj"
COPY . .
WORKDIR "/src/ProductService/FavoriteService/FavoriteAPI"
RUN dotnet build "./FavoriteAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FavoriteAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoriteAPI.dll"]
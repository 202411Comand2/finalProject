# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cart_Order_Service/CartService/CartApi/CartApi.csproj", "Cart_Order_Service/CartService/CartApi/"]
COPY ["Cart_Order_Service/CartService/CartService.BLL/CartService.BLL.csproj", "Cart_Order_Service/CartService/CartService.BLL/"]
COPY ["SupperBackEndDto/SupperBackEndDto.csproj", "SupperBackEndDto/"]
COPY ["Cart_Order_Service/CartService/CartService.DAL/CartService.DAL.csproj", "Cart_Order_Service/CartService/CartService.DAL/"]
COPY ["Cart_Order_Service/CartService/CartService.Domain/CartService.Domain.csproj", "Cart_Order_Service/CartService/CartService.Domain/"]
RUN dotnet restore "./Cart_Order_Service/CartService/CartApi/CartApi.csproj"
COPY . .
WORKDIR "/src/Cart_Order_Service/CartService/CartApi"
RUN dotnet build "./CartApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CartApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartApi.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CoffeeBreak.Api/CoffeeBreak.Api.csproj", "src/CoffeeBreak.Api/"]
COPY ["src/CoffeeBreak.Application/CoffeeBreak.Application.csproj", "src/CoffeeBreak.Application/"]
COPY ["src/CoffeeBreak.Domain/CoffeeBreak.Domain.csproj", "src/CoffeeBreak.Domain/"]
COPY ["src/CoffeeBreak.Infrastructure/CoffeeBreak.Infrastructure.csproj", "src/CoffeeBreak.Infrastructure/"]
RUN dotnet restore "./src/CoffeeBreak.Api/./CoffeeBreak.Api.csproj"
COPY . .
WORKDIR "/src/src/CoffeeBreak.Api"
RUN dotnet build "./CoffeeBreak.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./CoffeeBreak.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeBreak.Api.dll"]
# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CoffeeBreak.Api/CoffeeBreak.Api.csproj", "src/CoffeeBreak.Api/"]
COPY ["src/CoffeeBreak.Application/CoffeeBreak.Application.csproj", "src/CoffeeBreak.Application/"]
COPY ["src/CoffeeBreak.Domain/CoffeeBreak.Domain.csproj", "src/CoffeeBreak.Domain/"]
COPY ["src/CoffeeBreak.Infrastructure/CoffeeBreak.Infrastructure.csproj", "src/CoffeeBreak.Infrastructure/"]
RUN dotnet restore "./src/CoffeeBreak.Api/CoffeeBreak.Api.csproj"
COPY . .
WORKDIR "/src/src/CoffeeBreak.Api"
RUN dotnet build "./CoffeeBreak.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CoffeeBreak.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeBreak.Api.dll"]
# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ECommerce.Api/ECommerce.Api.csproj", "ECommerce.Api/"]
COPY ["src/ECommerce.Application/ECommerce.Application.csproj", "ECommerce.Application/"]
COPY ["src/ECommerce.Domain/ECommerce.Domain.csproj", "ECommerce.Domain/"]
COPY ["src/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "ECommerce.Infrastructure/"]
COPY ["src/ECommerce.Persistence/ECommerce.Persistence.csproj", "ECommerce.Persistence/"]
RUN dotnet restore "ECommerce.Api/ECommerce.Api.csproj"
COPY . ./ECommerce.Api
WORKDIR "/src/ECommerce.Api"
RUN dotnet build "ECommerce.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AMS.Api/AMS.Api.csproj", "AMS.Api/"]
COPY ["AMS.Application/AMS.Application.csproj", "AMS.Application/"]
COPY ["AMS.Domain/AMS.Domain.csproj", "AMS.Domain/"]
COPY ["AMS.Infrastructure/AMS.Infrastructure.csproj", "AMS.Infrastructure/"]
RUN dotnet restore "./AMS.Api/AMS.Api.csproj"
COPY . .
WORKDIR "/src/AMS.Api"
RUN dotnet build "./AMS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AMS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AMS.Api.dll"]
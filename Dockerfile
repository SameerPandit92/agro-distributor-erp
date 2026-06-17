FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# आपकी सारी लेयर्स की .csproj फाइलें यहाँ कॉपी हो रही हैं
COPY ["AgroERP/AgroERP.csproj", "AgroERP/"]
COPY ["AgroERP.Application/AgroERP.Application.csproj", "AgroERP.Application/"]
COPY ["AgroERP.Domain/AgroERP.Domain.csproj", "AgroERP.Domain/"]
COPY ["AgroERP.Persistence/AgroERP.Persistence.csproj", "AgroERP.Persistence/"]
COPY ["AgroERP.Infrastructure/AgroERP.Infrastructure.csproj", "AgroERP.Infrastructure/"]
COPY ["AgroERP.Shared/AgroERP.Shared.csproj", "AgroERP.Shared/"]

RUN dotnet restore "AgroERP/AgroERP.csproj"
COPY . .
WORKDIR "/src/AgroERP"
RUN dotnet build "AgroERP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgroERP.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgroERP.dll"]
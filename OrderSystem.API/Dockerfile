FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrderSystem.API/OrderSystem.API.csproj", "OrderSystem.API/"]
RUN dotnet restore "OrderSystem.API/OrderSystem.API.csproj"
COPY . .
WORKDIR "/src/OrderSystem.API"
RUN dotnet build "OrderSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderSystem.API.dll"]

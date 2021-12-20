#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Delivery.Api/Delivery.Api.csproj", "src/Delivery.Api/"]
COPY ["src/Delivery.Infrastructure/Delivery.Infrastructure.csproj", "src/Delivery.Infrastructure/"]
COPY ["src/Delivery.Core/Delivery.Core.csproj", "src/Delivery.Core/"]
COPY ["src/Delivery.Shared/Delivery.Shared.csproj", "src/Delivery.Shared/"]
RUN dotnet restore "src/Delivery.Api/Delivery.Api.csproj"
COPY . .
WORKDIR "/src/src/Delivery.Api"
RUN dotnet build "Delivery.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delivery.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delivery.Api.dll"]

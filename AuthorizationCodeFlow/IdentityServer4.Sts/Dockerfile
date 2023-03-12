#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["AuthorizationCodeFlow.sln", "AuthorizationCodeFlow.sln"]
COPY ["IdentityServer4.Sts/IdentityServer4.Sts.csproj", "IdentityServer4.Sts/"]
COPY ["Service.Api/Service.Api.csproj", "Service.Api/"]
COPY ["Mvc.Client.WebApp/Mvc.Client.WebApp.csproj", "Mvc.Client.WebApp/"]
COPY "docker-compose.dcproj" "docker-compose.dcproj"
RUN dotnet restore "AuthorizationCodeFlow.sln"

COPY . .
WORKDIR "/src/IdentityServer4.Sts"
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityServer4.Sts.dll"]
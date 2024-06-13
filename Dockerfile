#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChatMenezes.Web/ChatMenezes.Web.csproj", "ChatMenezes.Web/"]
COPY ["ChatMenezes.Domain/ChatMenezes.Domain.csproj", "ChatMenezes.Domain/"]
COPY ["ChatMenezes.Core/ChatMenezes.Core.csproj", "ChatMenezes.Core/"]
COPY ["ChatMenezes.Infra.Data/ChatMenezes.Infra.Data.csproj", "ChatMenezes.Infra.Data/"]
RUN dotnet restore "./ChatMenezes.Web/ChatMenezes.Web.csproj"
COPY . .
WORKDIR "/src/ChatMenezes.Web"
RUN dotnet build "./ChatMenezes.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatMenezes.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatMenezes.Web.dll"]

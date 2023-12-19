#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API.LineTen/API.LineTen.csproj", "API.LineTen/"]
COPY ["Application.LineTen/Application.LineTen.csproj", "Application.LineTen/"]
COPY ["Domain.LineTen/Domain.LineTen.csproj", "Domain.LineTen/"]
COPY ["Persistence.LineTen/Persistence.LineTen.csproj", "Persistence.LineTen/"]
RUN dotnet restore "API.LineTen/API.LineTen.csproj"
COPY . .
WORKDIR "/src/API.LineTen"
RUN dotnet build "API.LineTen.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.LineTen.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.LineTen.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EmprestimoDVD.API/EmprestimoDVD.API.csproj", "EmprestimoDVD.API/"]
COPY ["Common.Functions/Common.Functions.csproj", "Common.Functions/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["Common.DTO/Common.DTO.csproj", "Common.DTO/"]
COPY ["Common.Models/Common.Domain.csproj", "Common.Models/"]
COPY ["EmprestimoDVD.Application/EmprestimoDVD.Application.csproj", "EmprestimoDVD.Application/"]
COPY ["EmprestimoDVD.Infrastructure/EmprestimoDVD.Infrastructure.csproj", "EmprestimoDVD.Infrastructure/"]
COPY ["EmprestimoDVD.Domain/EmprestimoDVD.Domain.csproj", "EmprestimoDVD.Domain/"]
COPY ["EmprestimoDVD.DTO/EmprestimoDVD.DTO.csproj", "EmprestimoDVD.DTO/"]
COPY ["EmprestimoDVD.Persistence/EmprestimoDVD.Persistence.csproj", "EmprestimoDVD.Persistence/"]
COPY ["Common.DataAccess/Common.DataAccess.csproj", "Common.DataAccess/"]
RUN dotnet restore "EmprestimoDVD.API/EmprestimoDVD.API.csproj"
COPY . .
WORKDIR "/src/EmprestimoDVD.API"
RUN dotnet build "EmprestimoDVD.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmprestimoDVD.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmprestimoDVD.API.dll"]
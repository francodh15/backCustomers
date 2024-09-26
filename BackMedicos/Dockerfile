#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#ENV DB_CONNECTION_STRING="Server=sqlServiceDocker;Database=Customers;User Id=sa;Password=backAPI@@1995;TrustServerCertificate=True"
#ENV DB_PASSWORD="backAPI@@1995"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BackMedicos/BackMedicos.csproj", "BackMedicos/"]
RUN dotnet restore "./BackMedicos/BackMedicos.csproj"
COPY . .
WORKDIR "/src/BackMedicos"
RUN dotnet build "./BackMedicos.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackMedicos.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackMedicos.dll"]
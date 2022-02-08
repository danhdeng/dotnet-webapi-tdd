#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["WebApplication-Docker/WebApplication-Docker.csproj", "WebApplication-Docker/"]
#RUN dotnet restore "WebApplication-Docker/WebApplication-Docker.csproj"
#COPY . .
#WORKDIR "/src/WebApplication-Docker"
#RUN dotnet build "WebApplication-Docker.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "WebApplication-Docker.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WebApplication-Docker.dll"]
#

#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./CloudCustomers.API/CloudCustomers.API.csproj" --disable-parallel
RUN dotnet publish "./CloudCustomers.API/CloudCustomers.API.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "CloudCustomers.API.dll"]

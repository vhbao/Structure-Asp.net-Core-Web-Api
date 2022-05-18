#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QuanLySoTaiChinh/QuanLySoTaiChinh.csproj", "QuanLySoTaiChinh/"]
RUN dotnet restore "QuanLySoTaiChinh/QuanLySoTaiChinh.csproj"
COPY . .
WORKDIR "/src/QuanLySoTaiChinh"
RUN dotnet build "QuanLySoTaiChinh.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuanLySoTaiChinh.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuanLySoTaiChinh.dll"]
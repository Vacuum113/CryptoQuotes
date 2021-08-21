#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Receipts.Api/Receipts.Api.csproj", "Receipts.Api/"]
COPY ["Receipts.Application/Receipts.Application.csproj", "Receipts.Application/"]
COPY ["Receipts.Domain/Receipts.Domain.csproj", "Receipts.Domain/"]
COPY ["Receipts.Provider/Receipts.Provider.csproj", "Receipts.Provider/"]
COPY ["Receipts.Infrastructure/Receipts.Infrastructure.csproj", "Receipts.Infrastructure/"]
RUN dotnet restore "Receipts.Api/Receipts.Api.csproj"
COPY . .
WORKDIR "/src/Receipts.Api"
RUN dotnet build "Receipts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Receipts.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Receipts.Api.dll"]
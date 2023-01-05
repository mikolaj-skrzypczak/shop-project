FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Shop.Web/Shop.Web.csproj", "Shop.Web/"]
RUN dotnet restore "Shop.Web/Shop.Web.csproj"
COPY . .
WORKDIR "/src/Shop.Web"
RUN dotnet build "Shop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Web.dll"]

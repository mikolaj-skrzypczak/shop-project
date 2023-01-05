FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Shop.Admin/Shop.Admin.csproj", "Shop.Admin/"]
RUN dotnet restore "Shop.Admin/Shop.Admin.csproj"
COPY . .
WORKDIR "/src/Shop.Admin"
RUN dotnet build "Shop.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Admin.dll"]

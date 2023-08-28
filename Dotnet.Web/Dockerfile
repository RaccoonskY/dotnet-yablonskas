FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dotnet.Web/Dotnet.Web.csproj", "Dotnet.Web/"]
RUN dotnet restore "Dotnet.Web/Dotnet.Web.csproj"
COPY . .
WORKDIR "/src/Dotnet.Web"
RUN dotnet build "Dotnet.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Web.dll"]

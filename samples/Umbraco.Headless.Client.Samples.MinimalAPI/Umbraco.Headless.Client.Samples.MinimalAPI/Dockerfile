FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Umbraco.Headless.Client.Samples.MinimalAPI.csproj", "Umbraco.Headless.Client.Samples.MinimalAPI/"]
RUN dotnet restore "Umbraco.Headless.Client.Samples.MinimalAPI/Umbraco.Headless.Client.Samples.MinimalAPI.csproj"
WORKDIR "/src/Umbraco.Headless.Client.Samples.MinimalAPI"
COPY . ./
RUN dotnet build "Umbraco.Headless.Client.Samples.MinimalAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Umbraco.Headless.Client.Samples.MinimalAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Umbraco.Headless.Client.Samples.MinimalAPI.dll

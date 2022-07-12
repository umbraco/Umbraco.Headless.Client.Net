FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Umbraco.Headless.Client.Samples.MVC.csproj", "Umbraco.Headless.Client.Samples.MVC/"]
RUN dotnet restore "Umbraco.Headless.Client.Samples.MVC/Umbraco.Headless.Client.Samples.MVC.csproj"
WORKDIR "/src/Umbraco.Headless.Client.Samples.MVC"
COPY . ./
RUN dotnet build "Umbraco.Headless.Client.Samples.MVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Umbraco.Headless.Client.Samples.MVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Umbraco.Headless.Client.Samples.MVC.dll

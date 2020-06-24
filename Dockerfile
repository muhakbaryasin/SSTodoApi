FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY src /app
WORKDIR /app

RUN dotnet restore --configfile NuGet.Config
WORKDIR /app
RUN dotnet build -c Release -o /app/build

FROM build-env AS publish-env
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=publish-env /app/publish .
ENTRYPOINT ["dotnet", "TodoApi.dll"]
# Build Time
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#USER app
WORKDIR /app
COPY . ./

RUN dotnet restore
RUN dotnet publish -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled-extra
WORKDIR /app

COPY --from=build /app/out .
USER app
ENTRYPOINT ["dotnet", "LibraryMgmt.dll"]
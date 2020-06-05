# Get sdk image and build app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN apt update && apt install -y nmap
WORKDIR /app
COPY --from=build-env /app/out .
RUN mkdir Data
ENTRYPOINT ["dotnet", "Kamiizumi.NetworkDeviceScanner.Web.dll"]

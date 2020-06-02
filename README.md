# Network Device Scanner

Network Device Scanner is a web application that scans and logs active hosts on a network.

## Requirements

- [Visual Studio 2019](https://visualstudio.microsoft.com/) or greater
- [.NET Core SDK 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0) or greater
- [Nmap](https://nmap.org/) installed and on the path
    > This can be tested by running `nmap` at a terminal. Nmap's help should be displayed.

## Configuration

The application needs to be configured before being used. Configuration is handled via the standard ASP.NET Core configuration patterns, so can be provided via appsettings files or environment variables. The following settings must be configured before use:

- `TargetSpecification` - The subnet to scan (e.g. `192.168.1.0/24`)

## Usage

The application can be built and run using Visual Studio 2019.

When running the application administrative permissions may be required - this is due to Nmap requiring elevation in some configurations.

The application is built on .NET Core / ASP.NET Core / Kestrel, so once built it can be manually run from a terminal using the `dotnet` command.

## Tech Stack

The app is an ASP.NET Core 3 Razor Components web application, using Entity Framework Core and SQLite to store and manage data. The app scans for hosts using [Nmap](https://nmap.org/).

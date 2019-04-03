# Network Device Scanner

Network Device Scanner is a web application that scans and logs active hosts on a network.

## Requirements

- [Visual Studio 2019](https://visualstudio.microsoft.com/) or greater
- [.NET Core SDK 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0) or greater
- [Nmap](https://nmap.org/) installed and on the path
    > This can be tested by running `nmap` at a terminal. Nmap's help should be displayed.
- Local network on the 10.0.0.0/24 subnet
    > *The subnet being scanned is currently hard-coded.*

## Usage

The application can be built and run using Visual Studio 2019.

When running the application administrative permissions may be required - this is due to Nmap requiring elevation in some configurations.

The application is built on .NET Core / ASP.NET Core / Kestrel, so once built it can be manually run from a terminal using the `dotnet` command.

## Tech Stack

The app is an ASP.NET Core 3 Razor Components web application, using Entity Framework Core and SQLite to store and manage data. The app scans for hosts using [Nmap](https://nmap.org/).

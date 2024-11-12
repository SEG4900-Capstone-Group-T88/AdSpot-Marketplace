![](https://github.com/SEG4900-Capstone-Group-T88/AdSpot-Marketplace-Server/actions/workflows/dotnet.yml/badge.svg)

# AdSpot-Server
.NET backend for AdSpot Marketplace

# Getting Started
- `docker compose up` to run application
> See [here](https://stackoverflow.com/questions/54738010/paths-asp-net-https-and-microsoft-usersecrets-not-shared-on-mac) if having problems with certificates and Docker on Mac
   
# Testing
- `dotnet test --configuration Release --verbosity normal --logger trx --collect "Code Coverage;Format=cobertura" --settings "AdSpot.Test/CodeCoverage.runsettings" ./AdSpot.sln`
- `dotnet tool install -g dotnet-reportgenerator-globaltool`
- `reportgenerator -reports:**/*.cobertura.xml -targetdir:coverage -reporttypes:Html`


# VS Code Setup
- Install ".NET Core Test Explorer" to run xunit tests in VS Code

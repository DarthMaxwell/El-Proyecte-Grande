FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /MBW.Server
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /MBW.Server
COPY --from=build /MBW.Server/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MBW.Server.dll"]
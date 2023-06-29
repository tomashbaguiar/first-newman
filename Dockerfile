FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as disctinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish -c release -o /app/publish

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 5232/tcp
ENV ASPNETCORE_URLS http://*:5232
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "FirstNewman.Api.dll" ]


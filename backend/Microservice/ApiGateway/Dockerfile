FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY backend/Microservice/ApiGateway/ApiGateway.csproj ApiGateway/
RUN dotnet restore ApiGateway/ApiGateway.csproj

COPY backend/Microservice/ApiGateway ApiGateway/
WORKDIR /app/ApiGateway
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:5013
EXPOSE 5013
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
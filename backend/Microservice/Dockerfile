# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the UserService and TextFileDataStore project files
COPY backend/Microservice/UserService/UserService.csproj UserService/
COPY backend/Microservice/TextFileDataStore/TextFileDataStore.csproj TextFileDataStore/

# Restore the dependencies for UserService (which includes TextFileDataStore)
RUN dotnet restore UserService/UserService.csproj

# Copy the remaining source code for both projects
COPY backend/Microservice/UserService/. UserService/
COPY backend/Microservice/TextFileDataStore/. TextFileDataStore/

# Publish the UserService project
WORKDIR /app/UserService
RUN dotnet publish UserService.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Setting up txt file for user data
RUN mkdir -p Data
COPY backend/Microservice/UserService/Data/user.txt Data/

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port (adjust if necessary)
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Set the entry point for the container
ENTRYPOINT ["dotnet", "UserService.dll"]

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the UserService and TextFileDataStore project files
COPY ["UserService/UserService.csproj", "UserService/"]
COPY ["TextFileDataStore/TextFileDataStore.csproj", "TextFileDataStore/"]

# Restore the dependencies for UserService (which includes TextFileDataStore)
RUN dotnet restore "UserService/UserService.csproj"

# Copy the remaining source code for both projects
COPY UserService/. UserService/
COPY TextFileDataStore/. TextFileDataStore/

# Publish the UserService project
WORKDIR "/src/UserService"
RUN dotnet publish "UserService.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Setting up txt file for user data
RUN mkdir -p Data
COPY UserService/Data/user.txt Data/

# Copy the published output from the build stage
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

# Expose the port (adjust if necessary)
EXPOSE 5000

# Set the entry point for the container
ENTRYPOINT ["dotnet", "UserService.dll"]

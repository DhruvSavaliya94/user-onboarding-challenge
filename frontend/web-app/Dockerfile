# Stage 1: Build the Angular application
FROM node:alpine AS build

# Setup the working directory
WORKDIR /usr/src/app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy other files and folders to the working directory
COPY . .

# Build Angular application in PROD mode
RUN npm run build --prod

# Stage 2: Serve the application using Nginx
FROM nginx:alpine

# Copy the built Angular app files to Nginx HTML folder
COPY --from=build /usr/src/app/dist/web-app/browser/ /usr/share/nginx/html

# Replace the default Nginx configuration with a custom one
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port Nginx will listen on
EXPOSE 8080

# Start the Nginx server
CMD ["nginx", "-g", "daemon off;"]


# # Expose port 80
# EXPOSE 80

# # Start Nginx server
# CMD ["nginx", "-g", "daemon off;"]
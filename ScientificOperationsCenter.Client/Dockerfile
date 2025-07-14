# Use Nginx as the base image
FROM nginx:stable-alpine 

# Copy static files
COPY ScientificOperationsCenter.Client/wwwroot /usr/share/nginx/html

# Configure needed mime types
COPY ScientificOperationsCenter.Client/mime.types /etc/nginx/mime.types

# Expose the port Nginx will run on
EXPOSE 80/tcp

# Run nginx
CMD ["nginx", "-g", "daemon off;"]

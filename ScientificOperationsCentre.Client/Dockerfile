# Use Nginx as the base image
FROM nginx:stable-alpine-slim

# Copy static files
COPY ScientificOperationsCentre.Client/wwwroot /usr/share/nginx/html

# Configure needed mime types
COPY ScientificOperationsCentre.Client/mime.types /etc/nginx/mime.types

# Copy default configuration
COPY ScientificOperationsCentre.Client/default.conf /etc/nginx/conf.d/default.conf

# Expose the port Nginx will run on
EXPOSE 9000/tcp

# Run nginx
CMD ["nginx", "-g", "daemon off;"]
# Use Nginx as the base image
FROM nginx:stable-alpine 

# Copy static files
#COPY ScientificOperationsCenter.Client/wwwroot /usr/share/nginx/html

# Copy custom Nginx configuration
#COPY ScientificOperationsCenter.Client/nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port Nginx will run on
EXPOSE 80/tcp

# Run nginx
CMD ["nginx", "-g", "daemon off;"]

# Use Nginx as the base image
FROM nginx:stable-alpine 
# AS base

# Copy static files
#COPY ScientificOperationsCenter.Client/wwwroot /usr/share/nginx/html

# Copy custom Nginx configuration
#COPY ScientificOperationsCenter.Client/nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port Nginx will run on
EXPOSE 80/tcp

# No need for a build stage since we're serving static files
# The final stage is just the base Nginx image
FROM base AS final

# Run nginx
CMD ["nginx", "-g", "daemon off;"]

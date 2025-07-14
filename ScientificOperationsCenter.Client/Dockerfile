# Use Nginx as the base image
FROM nginx:stable-alpine AS base

# Remove default Nginx configuration
RUN rm /etc/nginx/conf.d/default.conf

# Copy static files
COPY ScientificOperationsCenter.Client/wwwroot /usr/share/nginx/html

# Copy custom Nginx configuration
# COPY ScientificOperationsCenter.Client/nginx.conf /etc/nginx/conf.d/default.conf

# Set permissions
RUN chown -R nginx:nginx /usr/share/nginx/html

# Expose the port Nginx will run on
EXPOSE 80/tcp

# No need for a build stage since we're serving static files
# The final stage is just the base Nginx image
FROM base AS final

# Use nginx user
USER nginx

# Run nginx
CMD ["nginx", "-g", "daemon off;"]

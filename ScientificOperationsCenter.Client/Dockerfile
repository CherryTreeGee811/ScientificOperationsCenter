# Use Nginx as the base image
FROM nginx:alpine AS base
COPY ./wwwroot /usr/share/nginx/html

# Expose the port Nginx will run on
EXPOSE 9000

# No need for a build stage since we're serving static files
# The final stage is just the base Nginx image
FROM base AS final

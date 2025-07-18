# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8000


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ScientificOperationsCentre.Api/ScientificOperationsCentre.Api.csproj", "ScientificOperationsCentre.Api/"]
RUN dotnet restore "./ScientificOperationsCentre.Api/ScientificOperationsCentre.Api.csproj"
COPY . .
WORKDIR "/src/ScientificOperationsCentre.Api"
RUN dotnet build "./ScientificOperationsCentre.Api.csproj" \
  -c "$BUILD_CONFIGURATION" \
  -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ScientificOperationsCentre.Api.csproj" \
  -c "$BUILD_CONFIGURATION" \
  -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy SSL certificate and key into the image
COPY ScientificOperationsCentre.Api/cert.pem ./
COPY ScientificOperationsCentre.Api/key.pem ./
USER root
# Set permissions so the 'app' user can read the cert and key
RUN chmod 644 cert.pem key.pem
USER app
ENTRYPOINT ["dotnet", "ScientificOperationsCentre.Api.dll"]
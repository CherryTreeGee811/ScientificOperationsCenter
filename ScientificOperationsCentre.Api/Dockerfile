# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8000/tcp


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
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
COPY ScientificOperationsCentre.Api/cert.pem /app/cert.pem
COPY ScientificOperationsCentre.Api/key.pem /app/key.pem
USER root
RUN chown app:app /app/cert.pem /app/key.pem
RUN chmod 600 /app/cert.pem /app/key.pem
USER app
ENTRYPOINT ["dotnet", "ScientificOperationsCentre.Api.dll"]

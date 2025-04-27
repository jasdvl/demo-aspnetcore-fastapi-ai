# Learn how to customize your debug container and how Visual Studio uses this Dockerfile 
# to build images for faster debugging at https://aka.ms/customizecontainer.

# This stage is used when running from Visual Studio in fast mode (default for Debug configuration).
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AIxplorer.Vision.Interpretation/AIxplorer.Vision.Interpretation.csproj", "AIxplorer/"]
RUN dotnet restore "./AIxplorer.Vision.Interpretation/AIxplorer.Vision.Interpretation.csproj"
COPY . .
WORKDIR "/src/AIxplorer.Vision.Interpretation"
RUN dotnet build "./AIxplorer.Vision.Interpretation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project, which will be copied into the final stage.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AIxplorer.Vision.Interpretation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from Visual Studio in regular mode 
# (default if not using the Debug configuration).
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIxplorer.Vision.Interpretation.dll"]

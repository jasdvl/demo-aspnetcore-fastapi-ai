# Learn how to customize your debug container and how Visual Studio uses this Dockerfile 
# to create images for faster debugging at https://aka.ms/customizecontainer.

# This stage is used when executed by Visual Studio in Fast Mode (default setting for Debug configuration).
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AIxplorer.Nlp.QnA/AIxplorer.Nlp.QnA.csproj", "AIxplorer.Nlp.QnA/"]
RUN dotnet restore "./AIxplorer.Nlp.QnA/AIxplorer.Nlp.QnA.csproj"
COPY . .
WORKDIR "/src/AIxplorer.Nlp.QnA"
RUN dotnet build "./AIxplorer.Nlp.QnA.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project, which will be copied to the final stage.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AIxplorer.Nlp.QnA.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when Visual Studio runs in regular mode 
# (default when the Debug configuration is not used).
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIxplorer.Nlp.QnA.dll"]

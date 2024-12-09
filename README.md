# AIxplorer

![AIxplorer](./assets/architecture-overview.png)

## Project Overview

**AIxplorer** is a sample application designed to demonstrate the integration of artificial intelligence (AI) in a modern web application. The project features an Angular-based frontend (`aixplorer.client`) and an ASP.NET Core backend (`AIxplorer.Server`).

**Note:**  
This project is for demonstration purposes only and is not intended for production use. Ensure that sensitive information is stored securely (e.g., in a Key Store).

### Key Components

1. **AIxplorer.Server (ASP.NET Core)**  
   The backend that handles AI processing, and provides an API for the frontend to interact with.

2. **aixplorer.client (Angular)**  
   The frontend of the application built with Angular, which allows users to interact with the backend services.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/) (optional, for containerization)

## Setting up HTTPS Certificates (Windows)

To enable HTTPS support for `AIxplorer.Server` when running in Docker, you need to create and trust an SSL certificate. Follow these steps:

1. **Create a development certificate** (if you haven't already):

    ```bash
    dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\AIxplorer.Server.pfx -p <your_password_here>
    dotnet dev-certs https --trust
    ```

This mounts the certificate from your local machine into the container during the Docker build and run process, ensuring that the ASP.NET Core server in the container can use it for HTTPS connections.

## Getting Started

### Option 1: Start the Application with dotnet and Angular Commands

1. Clone the repository:

    ```bash
    git clone https://github.com/jasdvl/sample-aspnetcore-ai.git
    ```

2. Navigate to the project directory:

    ```bash
    cd sample-aspnetcore-ai
    ```

3. Navigate to the Angular frontend directory and install dependencies:

    ```bash
    cd aixplorer.client
    npm install
    ```

4. Start the Angular application:

    ```bash
    ng serve
    ```

   This will start the Angular frontend on `http://localhost:4200` by default.

5. In a new terminal window, navigate to the ASP.NET Core backend directory:

    ```bash
    cd ../AIxplorer.Server
    ```

6. Restore the ASP.NET Core project dependencies:

    ```bash
    dotnet restore
    ```

7. Run the ASP.NET Core backend application:

    ```bash
    dotnet run
    ```

   This will start the ASP.NET Core backend on `https://localhost:5001` by default.

Now, your Angular frontend and ASP.NET Core backend should be running, with the frontend accessible via `http://localhost:4200` and the backend via `https://localhost:5001`.

### Option 2: Start All Services with Docker Compose

1. Clone the repository if you haven’t already:

    ```bash
    git clone https://github.com/jasdvl/sample-aspnetcore-ai.git
    ```

2. Navigate to the project directory:

    ```bash
    cd sample-aspnetcore-ai/src
    ```

3. Start all services (including database and microservices) with Docker Compose:

    ```bash
    docker-compose up --build -d
    ```

This command will build and start the entire application stack.


## Project Structure

- `AIxplorer.Server`: Blazor Web App

- `aixplorer.client`: gRPC Service

## TODO List

### Priority 1

### Priority 2

### Priority 3

## Branching Strategy

Since I am the sole developer on this project, I primarily work on the `main` branch. I prefer to keep things simple by committing directly to `main` for most tasks. However, if a new feature requires multiple related commits or substantial changes, I will create feature branches to manage those updates. Once the feature is complete, the branch will be merged back into `main`. My goal is to keep the main branch stable and up to date.

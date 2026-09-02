# 1. Build & Publish Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files for caching restore layer
COPY ["PresentationLayer/PresentationLayer.csproj", "PresentationLayer/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DatabaseLogicLayer/DatabaseLogicLayer.csproj", "DatabaseLogicLayer/"]

RUN dotnet restore "PresentationLayer/PresentationLayer.csproj"

# Copy all source files
COPY . .

# Publish directly
WORKDIR "/src/PresentationLayer"
RUN dotnet publish "PresentationLayer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PresentationLayer.dll"]
# Етап побудови (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копіюємо всі файли проектів для відновлення залежностей
COPY ["Catalog/API.csproj", "Catalog/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DatabaseAccessLayer/DatabaseAccessLayer.csproj", "DatabaseAccessLayer/"]

# Відновлюємо залежності для всього рішення
RUN dotnet restore "Catalog/API.csproj"

# Копіюємо ВЕСЬ вихідний код
COPY . .

# Будуємо проект
WORKDIR "/src/Catalog"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Етап публікації (важливо: робимо publish саме тут)
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Фінальний етап (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
# Копіюємо все, що було опубліковано, включаючи всі DLL залежностей
COPY --from=publish /app/publish .

# ПЕРЕВІР: назва файлу має точно збігатися з вихідною DLL
ENTRYPOINT ["dotnet", "API.dll"]
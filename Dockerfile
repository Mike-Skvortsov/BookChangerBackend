# Використовуємо SDK образ для побудови проєкту
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копіюємо лише .csproj файли для restore залежностей
COPY BooksChanger/*.csproj BooksChanger/
COPY BusinessLogic/*.csproj BusinessLogic/
COPY Database/*.csproj Database/

# Відновлюємо залежності
RUN dotnet restore BooksChanger/BooksChanger.csproj

# Збираємо проєкт
WORKDIR /app/BooksChanger
RUN dotnet publish -c Release -o /publish

# Використовуємо Runtime-образ для запуску
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /publish .

# Відкриваємо порт для сервера
EXPOSE 5000

# Команда запуску
CMD ["dotnet", "BooksChanger.dll"]

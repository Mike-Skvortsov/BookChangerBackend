# Крок 1: Використовуємо SDK-образ для збірки проєкту
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Налаштовуємо робочу директорію
WORKDIR /app

# Копіюємо всі .csproj файли з правильною структурою
COPY BooksChanger/BooksChanger/*.csproj ./BooksChanger/
COPY BooksChanger/BusinessLogic/*.csproj ./BusinessLogic/
COPY BooksChanger/Database/*.csproj ./Database/

# Виконуємо restore залежностей
RUN dotnet restore ./BooksChanger/BooksChanger.csproj

# Копіюємо решту файлів з проєкту
COPY BooksChanger/BooksChanger/ ./BooksChanger/
COPY BooksChanger/BusinessLogic/ ./BusinessLogic/
COPY BooksChanger/Database/ ./Database/

# Публікуємо застосунок
WORKDIR /app/BooksChanger
RUN dotnet publish -c Release -o /publish

# Крок 2: Використовуємо Runtime-образ для запуску застосунку
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Налаштовуємо робочу директорію
WORKDIR /app

# Копіюємо зібрані артефакти з попереднього етапу
COPY --from=build-env /publish .

# Відкриваємо порт для застосунку
EXPOSE 5000

# Вказуємо команду запуску
CMD ["dotnet", "BooksChanger.dll"]

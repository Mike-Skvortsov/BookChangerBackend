# Вказуємо SDK для збірки
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копіюємо лише необхідні файли
COPY ./BooksChanger/BooksChanger/*.csproj ./BooksChanger/
COPY ./BooksChanger/BusinessLogic/*.csproj ./BusinessLogic/
COPY ./BooksChanger/Database/*.csproj ./Database/

# Відновлюємо залежності
RUN dotnet restore ./BooksChanger/BooksChanger.csproj

# Копіюємо весь код
COPY ./BooksChanger/BooksChanger/ ./BooksChanger/
COPY ./BooksChanger/BusinessLogic/ ./BusinessLogic/
COPY ./BooksChanger/Database/ ./Database/

# Публікуємо застосунок
WORKDIR /app/BooksChanger
RUN dotnet publish -c Release -o /publish

# Використовуємо Runtime-образ
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /publish .

# Відкриваємо порт
EXPOSE 5000

# Запускаємо застосунок
CMD ["dotnet", "BooksChanger.dll"]

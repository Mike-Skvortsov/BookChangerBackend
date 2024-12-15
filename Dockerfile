# Вказуємо базовий образ для побудови
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копіюємо .csproj файли до відповідних папок
COPY BooksChanger/BooksChanger/BooksChanger.csproj BooksChanger/
COPY BooksChanger/BusinessLogic/BusinessLogic.csproj BusinessLogic/
COPY BooksChanger/Database/Database.csproj Database/

# Restore залежності
RUN dotnet restore BooksChanger/BooksChanger.csproj

# Копіюємо весь проєкт
COPY BooksChanger/BooksChanger/ BooksChanger/
COPY BooksChanger/BusinessLogic/ BusinessLogic/
COPY BooksChanger/Database/ Database/

# Видаляємо непотрібні файли
RUN rm -rf BooksChanger/bin BooksChanger/obj \
           BusinessLogic/bin BusinessLogic/obj \
           Database/bin Database/obj

# Публікуємо проєкт
WORKDIR /app/BooksChanger
RUN dotnet publish -c Release -o out

# Використовуємо ASP.NET Core образ для запуску
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/BooksChanger/out .

# Відкриваємо порт
EXPOSE 5000
CMD ["dotnet", "BooksChanger.dll"]

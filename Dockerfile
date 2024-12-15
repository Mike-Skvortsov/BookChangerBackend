# Вказуємо базовий образ для побудови
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копіюємо .csproj файли для restore
COPY BooksChanger/BooksChanger.csproj ./BooksChanger/
COPY BusinessLogic/BusinessLogic.csproj ./BusinessLogic/
COPY Database/Database.csproj ./Database/

# Restore
RUN dotnet restore BooksChanger/BooksChanger.csproj

# Копіюємо всі файли проєкту
COPY BooksChanger/ ./BooksChanger/
COPY BusinessLogic/ ./BusinessLogic/
COPY Database/ ./Database/

# Видаляємо непотрібні файли перед збіркою
RUN rm -rf ./BooksChanger/bin ./BooksChanger/obj \
           ./BusinessLogic/bin ./BusinessLogic/obj \
           ./Database/bin ./Database/obj

# Публікуємо проєкт
WORKDIR /app/BooksChanger
RUN dotnet publish -c Release -o out

# Використовуємо ASP.NET Core образ для запуску
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/BooksChanger/out .

EXPOSE 5000
CMD ["dotnet", "BooksChanger.dll"]

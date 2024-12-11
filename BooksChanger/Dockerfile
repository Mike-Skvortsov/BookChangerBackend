# 1. Використовуємо .NET SDK для побудови
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# 2. Копіюємо та будуємо проєкт
COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

# 3. Використовуємо .NET Runtime для запуску
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# 4. Відкриваємо порт
EXPOSE 5000
EXPOSE 5001

CMD ["dotnet", "BooksChanger.dll"]

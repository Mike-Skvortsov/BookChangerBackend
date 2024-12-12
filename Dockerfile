FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./BooksChanger ./BooksChanger
COPY ./BusinessLogic ./BusinessLogic
COPY ./Database ./Database

RUN rm -rf ./BooksChanger/bin ./BooksChanger/obj \
           ./BusinessLogic/bin ./BusinessLogic/obj \
           ./Database/bin ./Database/obj

WORKDIR /app/BooksChanger
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0   
WORKDIR /app
COPY --from=build-env /app/BooksChanger/out .

EXPOSE 5000

CMD ["dotnet", "BooksChanger.dll"]

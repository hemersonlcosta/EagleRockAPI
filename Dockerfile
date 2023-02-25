FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./TodoApi/TodoApi.csproj" --disable-parallel
RUN dotnet publish "./TodoApi/TodoApi.csproj" -c release -o /app --no-restore
RUN dotnet test

FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000




ENTRYPOINT [ "dotnet", "TodoApi.dll" ]
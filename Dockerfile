FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build
WORKDIR /source
COPY ["./TodoApi/TodoApi.csproj", "./TodoApi/TodoApi.csproj"]
RUN dotnet restore "./TodoApi/TodoApi.csproj" 

COPY . .
WORKDIR /source/TodoApi
RUN dotnet build "TodoApi.csproj" -c release -o /app/build

RUN dotnet test


FROM build AS publish
RUN dotnet publish "TodoApi.csproj" -c release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000


ENTRYPOINT [ "dotnet", "TodoApi.dll" ]
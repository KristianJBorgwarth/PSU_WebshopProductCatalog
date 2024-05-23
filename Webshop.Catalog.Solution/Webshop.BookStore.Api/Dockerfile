FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Webshop.BookStore.Api/Webshop.BookStore.Api.csproj", "Webshop.BookStore.Api/"]
RUN dotnet restore "Webshop.BookStore.Api/Webshop.BookStore.Api.csproj"
COPY . .
WORKDIR "/src/Webshop.BookStore.Api"
RUN dotnet build "Webshop.BookStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Webshop.BookStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webshop.BookStore.Api.dll"]

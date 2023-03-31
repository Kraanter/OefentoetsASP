FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StudentType/StudentType.csproj", "StudentType/"]
RUN dotnet restore "StudentType/StudentType.csproj"
COPY . .
WORKDIR "/src/StudentType"
RUN dotnet build "StudentType.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentType.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentType.dll"]

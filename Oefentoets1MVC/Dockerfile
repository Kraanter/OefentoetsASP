FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Oefentoets1MVC/Oefentoets1MVC.csproj", "Oefentoets1MVC/"]
RUN dotnet restore "Oefentoets1MVC/Oefentoets1MVC.csproj"
COPY . .
WORKDIR "/src/Oefentoets1MVC"
RUN dotnet build "Oefentoets1MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oefentoets1MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oefentoets1MVC.dll"]

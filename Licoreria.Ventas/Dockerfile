FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Ventas.Domain/Ventas.Domain.csproj Ventas.Domain/
COPY Ventas.Application/Ventas.Application.csproj Ventas.Application/
COPY Ventas.Infrastructure/Ventas.Infrastructure.csproj Ventas.Infrastructure/
COPY Ventas.API/Ventas.API.csproj Ventas.API/
COPY Licoreria.Ventas.sln ./

RUN dotnet restore "Ventas.API/Ventas.API.csproj"

COPY . .

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ventas.API.dll"]
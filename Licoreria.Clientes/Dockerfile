FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Clientes.Domain/Clientes.Domain.csproj Clientes.Domain/
COPY Clientes.Application/Clientes.Application.csproj Clientes.Application/
COPY Clientes.Infrastructure/Clientes.Infrastructure.csproj Clientes.Infrastructure/
COPY Clientes.API/Clientes.API.csproj Clientes.API/
COPY Licoreria.Clientes.sln ./

RUN dotnet restore "Clientes.API/Clientes.API.csproj"

COPY . .

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Clientes.API.dll"]
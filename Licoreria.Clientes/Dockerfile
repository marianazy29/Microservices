FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Clientes.Domain/Clientes.Domain.csproj ../Clientes.Domain/
COPY Clientes.Application/Clientes.Application.csproj ../Clientes.Application/
COPY Clientes.Infrastructure/Clientes.Infrastructure.csproj ../Clientes.Infrastructure/
COPY Clientes.API.csproj ./
RUN dotnet restore Clientes.API.csproj

COPY . .
COPY Clientes.Domain/ ../Clientes.Domain/
COPY Clientes.Application/ ../Clientes.Application/
COPY Clientes.Infrastructure/ ../Clientes.Infrastructure/
RUN dotnet publish Clientes.API.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Clientes.API.dll"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Ventas.Domain/Ventas.Domain.csproj ../Ventas.Domain/
COPY Ventas.Application/Ventas.Application.csproj ../Ventas.Application/
COPY Ventas.Infrastructure/Ventas.Infrastructure.csproj ../Ventas.Infrastructure/
COPY Ventas.API.csproj ./
RUN dotnet restore Ventas.API.csproj

COPY . .
COPY Ventas.Domain/ ../Ventas.Domain/
COPY Ventas.Application/ ../Ventas.Application/
COPY Ventas.Infrastructure/ ../Ventas.Infrastructure/
RUN dotnet publish Ventas.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ventas.API.dll"]
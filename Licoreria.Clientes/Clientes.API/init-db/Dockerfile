FROM mcr.microsoft.com/mssql/server:2022-latest

ENV SA_PASSWORD=System32
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Express

# Copiar el script de inicialización
COPY init.sql /usr/src/app/init.sql

# Copiar script para ejecutar el SQL después de que SQL Server arranque
COPY entrypoint.sh /usr/src/app/entrypoint.sh
RUN chmod +x /usr/src/app/entrypoint.sh

ENTRYPOINT ["/usr/src/app/entrypoint.sh"]
#!/bin/bash
/opt/mssql/bin/sqlservr &

# Esperar a que SQL Server inicie (unos segundos)
sleep 20

# Ejecutar el script de inicialización
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -i /usr/src/app/init.sql

# Mantener el contenedor vivo
wait
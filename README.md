# Distribu-Te

## MSSQL
Service name - distribu-te-mssql-db
This has the database required for the Distribu-Te application. 

## lib-infra-app-database
Migration for sql

dotnet ef migrations script 0 20250223214443_InitialCreate --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250223214443_InitialCreate.sql" 

dotnet ef migrations script 20250223214443_InitialCreate 0 --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250223214443_InitialCreate.sql"
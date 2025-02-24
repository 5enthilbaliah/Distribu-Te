# Distribu-Te

## MSSQL
Service name - distribu-te-mssql-db
This has the database required for the Distribu-Te application. 

## lib-infra-app-database
Migration for sql

dotnet ef migrations script 0 20250223214443_InitialCreate 20250223231229_SquadAssociates --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250223214443_InitialCreate.sql"
dotnet ef migrations script 20250223214443_InitialCreate 0 --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250223214443_InitialCreate.sql"

dotnet ef migrations script 20250223214443_InitialCreate 20250223231229_SquadAssociates --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250223231229_SquadAssociates.sql"
dotnet ef migrations script 20250223231229_SquadAssociates 20250223214443_InitialCreate --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250223231229_SquadAssociates.sql"

dotnet ef migrations script 20250223231229_SquadAssociates 20250224014453_SquadProjects --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224014453_SquadProjects.sql"
dotnet ef migrations script 20250224014453_SquadProjects 20250223231229_SquadAssociates --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224014453_SquadProjects.sql"

dotnet ef migrations script 20250224014453_SquadProjects 20250224022218_Environments --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224022218_Environments.sql"
dotnet ef migrations script 20250224022218_Environments 20250224014453_SquadProjects --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224022218_Environments.sql"

dotnet ef migrations script 20250224022218_Environments 20250224025046_Deployments --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224025046_Deployments.sql"
dotnet ef migrations script 20250224025046_Deployments 20250224022218_Environments --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224025046_Deployments.sql"

dotnet ef migrations script 20250224025046_Deployments 20250224030537_DeploymentTaskTypes --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224030537_DeploymentTaskTypes.sql"
dotnet ef migrations script 20250224030537_DeploymentTaskTypes 20250224025046_Deployments --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224030537_DeploymentTaskTypes.sql"

dotnet ef migrations script 20250224030537_DeploymentTaskTypes 20250224033424_DeploymentItems --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224033424_DeploymentItems.sql"
dotnet ef migrations script 20250224033424_DeploymentItems 20250224030537_DeploymentTaskTypes --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224033424_DeploymentItems.sql"

dotnet ef migrations script 20250224033424_DeploymentItems 20250224040213_DeploymentItemTasks --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224040213_DeploymentItemTasks.sql"
dotnet ef migrations script 20250224040213_DeploymentItemTasks 20250224033424_DeploymentItems --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224040213_DeploymentItemTasks.sql"

dotnet ef migrations script 20250224040213_DeploymentItemTasks 20250224042650_DeploymentStatuses --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224042650_DeploymentStatuses.sql"
dotnet ef migrations script 20250224042650_DeploymentStatuses 20250224040213_DeploymentItemTasks --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224042650_DeploymentStatuses.sql"

dotnet ef migrations script 20250224042650_DeploymentStatuses 20250224051032_DeploymentStatusesForeignKeys --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224051032_DeploymentStatusesForeignKeys.sql"
dotnet ef migrations script 20250224051032_DeploymentStatusesForeignKeys 20250224042650_DeploymentStatuses --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224051032_DeploymentStatusesForeignKeys.sql"

dotnet ef migrations script 20250224051032_DeploymentStatusesForeignKeys 20250224051843_DeploymentStatusesForeignKeys2 --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224051843_DeploymentStatusesForeignKeys2.sql"
dotnet ef migrations script 20250224051843_DeploymentStatusesForeignKeys2 20250224051032_DeploymentStatusesForeignKeys --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224051843_DeploymentStatusesForeignKeys2.sql"

dotnet ef migrations script 20250224051843_DeploymentStatusesForeignKeys2 20250224052633_DeploymentStatusesForeignKeys3 --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\execution-script\20250224052633_DeploymentStatusesForeignKeys3.sql"
dotnet ef migrations script 20250224052633_DeploymentStatusesForeignKeys3 20250224051843_DeploymentStatusesForeignKeys2 --idempotent --context DistribuTeDbContext --output "E:\Distribu-Te\lib-infra-app-database\migrations\rollback-script\20250224052633_DeploymentStatusesForeignKeys3.sql"
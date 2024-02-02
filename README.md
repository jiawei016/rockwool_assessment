# rockwool_assessment

### installation
| Tools | Version |
| ----------- | ----------- |
| Docker Desktop | Latest |
| Node.js | v19 |
| React | v18 |
| .NET | 8.0 |

### commands to run if using docker
*SQL Server Installation*
> `docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=P@ssword123" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/mssql/server`
> 
*Docker Compose Environment Installation*

**Step 1**
Open Command Prompt
> run this command
>
> `cd <project_folder>`

**Step 2** 
> run this command
> 
> `docker-compose -f "docker-compose-env.yaml" up -d`

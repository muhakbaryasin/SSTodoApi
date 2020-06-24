# ToDoApi

Development Environment
-----------------------
- Windows 10 x64
- .Net Core SDK 3.1 + ServiceStack
- SQL Server 2017 Express Edition (running on ubuntu Docker)

Docker Environment
------------------
- Switch to branch docker

Docker Spec
-----------
- MS Sql Server 2017 Ubuntu
    >docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=123abcMetnah" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
- My To Do API 
	>docker run muhakbaryasin/sstodoapi
	https://hub.docker.com/r/muhakbaryasin/sstodoapi
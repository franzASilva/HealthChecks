# Result Pattern 
**Objective:** avoid using exception in business flow control
- [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview);
- Controllers API;
- ASP.NET Core pattern for healthcheck;
- health checks: Sql Server, remote access, in memory database, ...


![image](https://github.com/user-attachments/assets/75984404-7fb6-4e94-a15d-6bc52013834b)



**Use Swagger to help test**

![image](https://github.com/user-attachments/assets/d9b56be2-7f2a-4172-8554-044c83c533b8)


[Swagger](https://localhost:7291/swagger/index.html): Access and use enpoint to generate a list of dummies, this will save it in the memory db

![image](https://github.com/user-attachments/assets/19270294-5929-4b8c-aca6-1b95c80f93cb)


Back to [health check](https://localhost:7291/healthcheck) the in memory db will be healthy

![image](https://github.com/user-attachments/assets/4e002e8f-c1f9-46ba-8f55-ddb0b51c9f1c)


Comment the check to the local database

![image](https://github.com/user-attachments/assets/faaeb383-e365-4219-83c6-3a87be98defe)


Application (statusApplication) will be healthy

![image](https://github.com/user-attachments/assets/0a37d55b-7d95-42f9-aa79-e2a5a9a0eaf8)



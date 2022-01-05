# HoneyRestaurant
## II. Identity service
### 1. Nuget package in project
```
Duende.IdentityServer.AspNetIdentity
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity.UI
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```
### 2. [Duende docs](https://docs.duendesoftware.com/identityserver/v5)
- Preparation
``` dotnet new --install Duende.IdentityServer.Templates ```
- [Set up IdentityResource ScopeApi, Client](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.Identity/SD.cs)
- [Seeding user](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.Identity/Initializer/DbInitializer.cs)
> admin account
> ```id: admin@mail.com pass: Tt123` ```

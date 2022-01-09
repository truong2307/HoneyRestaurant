# HoneyRestaurant
## I. ProductAPI service
### 1. Nuget package in project
```
AutoMapper
AutoMapper.Extensions.Microsoft.DependencyInjection
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Swashbuckle.AspNetCore.Annotations
Swashbuckle.AspNetCore.SwaggerUI
```
### 3. In project
- Config automapper, dto
- [Setup repository, unit of work](https://github.com/truong2307/HoneyRestaurant/tree/master/Honey.Services.ProductAPI/Repository)
- [Config all services, jwt bearer, authentication, authorization](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.ProductAPI/Utility/ConfigDIServices.cs)
- [Controller api](https://github.com/truong2307/HoneyRestaurant/tree/master/Honey.Services.ProductAPI/Controllers)
- [seeding product](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.ProductAPI/DbContexts/ApplicationDbContext.cs)
- Protect resource with authorize
```c#
[Authorize(Roles = "Admin")]
public async Task<object> Function(object parameter){Implement}      
```
## II. Identity sever service
### 1. Nuget package in project
```
Duende.IdentityServer.AspNetIdentity
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity.UI
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```
### 2. In project
- [Duende docs](https://docs.duendesoftware.com/identityserver/v5)
- Preparation
``` dotnet new --install Duende.IdentityServer.Templates ```
- [Set up IdentityResource, ScopeApi, Client](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.Identity/SD.cs)
- [Seeding user](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.Identity/Initializer/DbInitializer.cs)
- [Implement login](https://github.com/truong2307/HoneyRestaurant/commit/3c4096828945ea1cbd0500ee1218e0a82afe1718), and [register](https://github.com/truong2307/HoneyRestaurant/commit/97f26548f41045f6c3a1cc59cf20a75f2058b12b)
- [Modify ClaimsPrincipal info user](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.Identity/Services/ProfileService.cs)
> admin account
```id: admin@mail.com pass: Tt123` ```

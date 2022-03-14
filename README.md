# HoneyRestaurant
## I. Honey.Web (Fe use MVC)
### HomePage
![homepage](https://drive.google.com/uc?export=view&id=1VmoGWYfu-3PAZ1JwaVgTXj6-BYAUdFsB)
### ShoppingCart
![ShoppingCart](https://drive.google.com/uc?export=view&id=1AyHldHaF8xjsE64SFJXduOSFG587jP3U)

### 1. Nuget package in project
```
Microsoft.AspNetCore.Authentication
Microsoft.AspNetCore.Authentication.OpenIdConnect
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Microsoft.VisualStudio.Web.CodeGeneration.Design
Newtonsoft.Json
System.IdentityModel.Tokens.Jwt
```
### 2. In project
- [Docs HttpContext class](https://www.c-sharpcorner.com/UploadFile/dacca2/httpcontext-class-in-Asp-Net/)
- [Implement base services httpClient](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Web/Services/BaseService.cs)
- [Implement entity service (product, category)](https://github.com/truong2307/HoneyRestaurant/commit/ce772cedd50092645c6fef75db8ddb95f53bc570)
- [Config all DI service, add Authentication from identitySever](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Web/Utility/ConfigDIService.cs)
- [Controller manage category, product, shoppingCart](https://github.com/truong2307/HoneyRestaurant/tree/master/Honey.Web/Controllers)
- [Pass token access to productApi to get resource](https://github.com/truong2307/HoneyRestaurant/commit/42f478fa9284b244b0d64c8389d3b1b44ae3871e)

## II. ProductAPI service
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
## III. Identity sever service
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
```c#
admin account test:
id: admin@mail.com pass: Tt123`
```
## IV. ShoppingCartAPI service
### 1. Nuget package in project
```
AutoMapper
AutoMapper.Extensions.Microsoft.DependencyInjection
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Newtonsoft.Json
Swashbuckle.AspNetCore
Swashbuckle.AspNetCore.Annotations
Swashbuckle.AspNetCore.SwaggerUI
```
### 2. In project
- [Implement CartRepository: create, update, remove, clear shoppingCart, get shoppingCart by userId](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.ShoppingCartAPI/Repository/CartRepository.cs)
- [Config all services lifetime, add authentication bearer](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.ShoppingCartAPI/Utility/ConfigDIServices.cs)
- [Controller shoppingCart endPoint](https://github.com/truong2307/HoneyRestaurant/blob/master/Honey.Services.ShoppingCartAPI/Controllers/CartController.cs)






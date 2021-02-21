# Linux CentOS 8 移动开发环境搭建
## 后端代码(Docker 启动 8082)
```
$ cd /usr/mybgapi/JPlatform/CleanArchitecture

Dockerfile(/usr/mybgapi/JPlatform/CleanArchitecture/Dockerfile)

/usr/mybgapi/JPlatform/CleanArchitecture
$ docker build . -t japi:0.0.1

$ podman run -it  -p 8082:80  --name jwebapi  c115092f38eb

http://8.135.118.30:8082/api/index.html?url=/api/specification.json#/
```



## 后端编译后文件(硬启动 8888)
```
/usr/src/webapipub
dotnet WebApi.dll --urls=https://*:8888
```


# uView 前端发布（80 默认端口）
```
/usr/share/nginx/myapprelease
http://8.135.118.30/
```



# CleanArchitecture
## Vue & Clean CleanArchitecture full stack solution

## this project based on following repo.
## BackEnd
### Matech
https://www.nuget.org/packages/Matech.Clean.Architecture.Template

### ardalis
https://github.com/ardalis
## FrontEnd
https://github.com/PanJiaChen/vue-element-admin


## CQRS
https://docs.microsoft.com/zh-tw/azure/architecture/patterns/cqrs

## DDD Clean Architecture
![CleanArchitecture](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/CleanArchitecture.png)

## Technologies
.NET 5.0.x
ASP .NET 5.0.x
Entity Framework Core 5.0.x
MediatR
Mapster
FluentValidation
NUnit, FluentAssertions, Moq & Respawn
Elasticsearch, Serilog, Kibana
Project screenshot

## Project Structure
![CodeStructure](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/CodeStructure.png)

## Get Start (change the db to mysql)

* Add nuget Package 
`Install-Package Pomelo.EntityFrameworkCore.MySql -Version 5.0.0-alpha.2`

* DependencyInjection.cs 
```
services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseMySql(configuration.GetConnectionString("DefaultConnection_MySql"), new MySqlServerVersion(new Version(5, 6, 23))));
```

* Create Migrations & Generate DB 
```
dotnet ef migrations add "CreateDb" --project src\Common\Infrastructure --startup-project src\Apps\WebApi --output-dir Persistence\Migrations
dotnet ef database update --project src\Common\Infrastructure --startup-project src\Apps\WebApi
```

## Project Preview
### Login/Main form UI
#### Login
![Login](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Login.png)
#### Main
![Main](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Main.png)
#### Setting
![Setting](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Setting.png)

### Business UI(File Upload, multilevel menu, video)
#### Business
![Business](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Business.png)
#### Business
![Business2](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Business2.png)
#### Business
![Business3](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Business3.png)
#### Video Play Help Center
![Vedios](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Vedios.png)


## ELK Related
```
public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(
                            new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                            {
                                IndexFormat =
                                    $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                                AutoRegisterTemplate = true,
                                NumberOfShards = 2,
                                NumberOfReplicas = 1
                            })
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .ReadFrom.Configuration(context.Configuration);
                }))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
```

#### use docker-compose to start elk on docker-desktop
![Docker](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Docker.png)

#### ElasticSearch
![ElasticSearch](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/ElasticSearch.png)

#### Kiba
![Kiba](https://github.com/MorningstarJerry/CleanArchitecture/blob/main/ScreenShots/Kiba.png)

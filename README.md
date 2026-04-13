# Trip.Api

## 1. 项目介绍

这是一个基于 `.NET 10` 进行开发的 `RESTful` 风格的 `WebAPI` 项目，实现了如下功能及特性：

1. RESTful API
2. 关键词搜索
3. 数据过滤
4. 数据验证
5. JWT鉴权
6. Stateless状态机
7. 数据分页
8. 数据排序
9. 数据塑性
10. HATEOAS API
11. 容器化

## 2. 技术栈

1. `AspNetCore`: 基础的 `Web` 开发
2. `EntityFrameworkCore`: 数据访问
3. `Mapster`: ORM框架，实现数据自动映射(因为AutoMapper最新版本收费，故采用Mapster)
4. `Newtonsoft.Json`: 数据序列化/反序列化
5. `JSON Patch`: 为局部数据更新提供支持
6. `JWT Bearer`: 数据鉴权
7. `Stateless`: 为订单系统提供建议的状态机
8. `LINQ Dynamic`: 动态的LINQ查询

## 3. 编译&运行

编译项目，请使用如下命令：

```bash
dotnet bulid
```

运行项目，请使用如下命令:

```bash
dotnet run --project ./src/Trip.Api/Trip.Api.csproj
```

## 4. 联系我

关于本项目有任何疑问，请发送邮件至我的电子邮箱: `dawson09@163.com`，或者是提 Issues
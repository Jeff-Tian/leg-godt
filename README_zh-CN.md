# leg-godt 好好玩乐

---

> 我的 dotnet core 玩乐场

[English](./README.md) | [**简体中文**](./README_zh-CN.md)

## 部署目标

### Azure Web App

### Aliyun ECS

## 本地开发

### 运行测试

```shell
dotnet test
```

### 构建

```shell
dotnet build
```

### 运行

```shell
AWS_SECRET_ACCESS_KEY=1234 AWS_ACCESS_KEY_ID=5678 dotnet run --project Web/Web.csproj --urls "http://*:3002;https://*:3003"

open http://localhost:3002
```

## 查看日志

### Azure Insights

可以用来查看部署在 Azure Web App 上的应用请求日志和应用日志。

通过 [Azure Insights](https://portal.azure.com/#@JeffTianoutlook758.onmicrosoft.com/resource/subscriptions/90ae756c-a3a1-41a8-bcdf-e72efdadefd6/resourceGroups/leg-godt_group/providers/microsoft.insights/components/leg-godt/logs )，输入 `requests` 可以查看请求日志；输入 `traces` 可以查看应用日志。

![](./4f03c24e-05ae-45b9-b8a9-cbfe8e039f40.png)

### logit.io

为了学习一下可观测性，花了 108 美元每年购买了 logit.io 的服务，可以用来查看部署在 Aliyun ECS 上的应用请求日志和应用日志。

https://kibana.logit.io/s/59eb0e4e-3be4-46c3-8953-2e5d07b98b8a/app/data-explorer/discover

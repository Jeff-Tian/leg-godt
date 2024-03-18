# leg-godt

---

> A playground for my .dotnet core application.

## Local test

```bash
dotnet test --logger:"console;verbosity=normal"
```

## Local run

If you want to send log to a remote server, you can use the following command:

```bash
AWS_ACCESS_KEY_ID=x AWS_SECRET_ACCESS_KEY=y Serilog__WriteTo__0__Args__uri=tls://your-uri-here dotnet run --project Web/Web.csproj --urls "http://*:3002;https://*:3003"
```

Otherwise you can just run the following only write log to console:

```bash
AWS_ACCESS_KEY_ID=x AWS_SECRET_ACCESS_KEY=y dotnet run --project Web/Web.csproj --urls "http://*:3002;https://*:3003"
```

## Log stream

https://portal.azure.com/#@JeffTianoutlook758.onmicrosoft.com/resource/subscriptions/90ae756c-a3a1-41a8-bcdf-e72efdadefd6/resourceGroups/leg-godt_group/providers/Microsoft.Web/sites/leg-godt/logStream

## Dependencies

### Database

Need a postgresql database.

This sample demonstrates a tiny Hello World .NET Core app for [App Service Web App](https://docs.microsoft.com/azure/app-service-web). This sample can be used in a .NET Azure App Service app as well as in a Custom Container Azure App Service app.

## Log in to Azure Container Registry

Using the Azure CLI, log in to the Azure Container Registry (ACR):

```azurecli
az acr login -n <your_registry_name>
```

## Running in a Docker Container

This repository contains 2 Dockerfiles, a Linux container and a Windows container.

### Publish the Windows image to your Registry

To build the Windows image locally and publish to ACR, run the following command:

```docker
docker build -f Dockerfile.windows -t dotnetcore-docs-hello-world-windows . 
docker tag dotnetcore-docs-hello-world-windows <your_registry_name>.azurecr.io/dotnetcore-docs-hello-world-windows:latest
docker push <your_registry_name>.azurecr.io/dotnetcore-docs-hello-world-windows:latest
```

### Publish the Linux image to your Registry

To build the Windows image locally and publish to ACR, run the following command:

```docker
docker build -f Dockerfile.linux -t dotnetcore-docs-hello-world-linux . 
docker tag dotnetcore-docs-hello-world-windows <your_registry_name>.azurecr.io/dotnetcore-docs-hello-world-linux:latest
docker push <your_registry_name>.azurecr.io/dotnetcore-docs-hello-world-linux:latest
```

# Test

## Dockerfile

I made a Dockerfile for easy testing, especially for BDD testing. The image was uploaded to Docker hub, to update the image, run the following command:

```bash
docker build -t jefftian/dotnet-chrome:latest .
docker login -u jefftian -p <password>
docker push jefftian/dotnet-chrome:latest
```

# Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

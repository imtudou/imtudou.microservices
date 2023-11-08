#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#使用aspnet:5.0作为基础镜像，起一个别名为base
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#设置容器的工作目录为/app
WORKDIR /app
#暴露80端口
EXPOSE 80


#使用dotnet sdk:5.0作为基础镜像，起一个别名为build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#设置容器的工作目录为/src
WORKDIR /src
#拷贝src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Imtudou.IdentityServer.WebAPI.csproj项目文件到容器中的src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/目录
COPY ["src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Imtudou.IdentityServer.WebAPI.csproj", "src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/"]

#执行dotnet restore命令，相当于平时用vs还原nuget包
RUN dotnet restore "src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Imtudou.IdentityServer.WebAPI.csproj"
#拷贝当前目录的文件到到容器的/src目录
COPY . .
#设置容器的工作目录为/src/src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI
WORKDIR "/src/src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI"
#执行dotnet build命令，相当于平时用vs生成项目。以Release模式生成到容器的/app/build目录
RUN dotnet build "Imtudou.IdentityServer.WebAPI.csproj" -c Release -o /app/build

#将上面的build(dotnet sdk:5.0)作为基础镜像，又重命名为publish
FROM build AS publish
#执行dotnet publish命令，相当于平时用vs发布项目。以Release模式发布到容器的/app/publish目录
RUN dotnet publish "Imtudou.IdentityServer.WebAPI.csproj" -c Release -o /app/publish

#将上面的base(dotnet sdk:5.0)作为基础镜像，又重命名为final
FROM base AS final
#设置容器的工作目录为/app
WORKDIR /app
#拷贝/app/publish目录到当前工作目录
COPY --from=publish /app/publish .
#指定容器入口命令，容器启动时会运行dotnet Imtudou.IdentityServer.WebAPI.dll
ENTRYPOINT ["dotnet", "Imtudou.IdentityServer.WebAPI.dll"]
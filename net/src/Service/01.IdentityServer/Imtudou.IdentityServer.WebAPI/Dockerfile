#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#ʹ��aspnet:5.0��Ϊ����������һ������Ϊbase
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#���������Ĺ���Ŀ¼Ϊ/app
WORKDIR /app
#��¶80�˿�
EXPOSE 80


#ʹ��dotnet sdk:5.0��Ϊ����������һ������Ϊbuild
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#���������Ĺ���Ŀ¼Ϊ/src
WORKDIR /src
#����src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Imtudou.IdentityServer.WebAPI.csproj��Ŀ�ļ��������е�src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Ŀ¼
COPY ["src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Imtudou.IdentityServer.WebAPI.csproj", "src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/"]

#ִ��dotnet restore����൱��ƽʱ��vs��ԭnuget��
RUN dotnet restore "src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI/Imtudou.IdentityServer.WebAPI.csproj"
#������ǰĿ¼���ļ�����������/srcĿ¼
COPY . .
#���������Ĺ���Ŀ¼Ϊ/src/src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI
WORKDIR "/src/src/Service/01.IdentityServer/Imtudou.IdentityServer.WebAPI"
#ִ��dotnet build����൱��ƽʱ��vs������Ŀ����Releaseģʽ���ɵ�������/app/buildĿ¼
RUN dotnet build "Imtudou.IdentityServer.WebAPI.csproj" -c Release -o /app/build

#�������build(dotnet sdk:5.0)��Ϊ����������������Ϊpublish
FROM build AS publish
#ִ��dotnet publish����൱��ƽʱ��vs������Ŀ����Releaseģʽ������������/app/publishĿ¼
RUN dotnet publish "Imtudou.IdentityServer.WebAPI.csproj" -c Release -o /app/publish

#�������base(dotnet sdk:5.0)��Ϊ����������������Ϊfinal
FROM base AS final
#���������Ĺ���Ŀ¼Ϊ/app
WORKDIR /app
#����/app/publishĿ¼����ǰ����Ŀ¼
COPY --from=publish /app/publish .
#ָ��������������������ʱ������dotnet Imtudou.IdentityServer.WebAPI.dll
ENTRYPOINT ["dotnet", "Imtudou.IdentityServer.WebAPI.dll"]
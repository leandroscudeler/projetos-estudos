FROM microsoft/dotnet:2.1-aspnetcore-runtime

LABEL version="1.0.2" description="Aplicacao ASP .NET Core MVC"

COPY dist /app

WORKDIR /app

EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "mvc1.dll"]
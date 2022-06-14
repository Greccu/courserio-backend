FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY ["./build", "/app"]

EXPOSE 5000

ENTRYPOINT ["dotnet", "Courserio.Api.dll"]
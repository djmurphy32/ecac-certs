FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:10.0

ARG ARTIFACTS_DIR
WORKDIR /app
COPY $ARTIFACTS_DIR ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "Ecac.Certs.Api.dll"]
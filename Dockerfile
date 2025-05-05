# Etapa 1: Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar todos os arquivos de projeto (cada projeto deve estar no mesmo nível que a solution)
COPY ./MonolitoBackend.Api/MonolitoBackend.Api.csproj ./MonolitoBackend.Api/
COPY ./MonolitoBackend.Core/MonolitoBackend.Core.csproj ./MonolitoBackend.Core/
COPY ./MonolitoBackend.infra/MonolitoBackend.infra.csproj ./MonolitoBackend.infra/


# Restaurar dependências
RUN dotnet restore ./MonolitoBackend.Api/MonolitoBackend.Api.csproj

# Copiar o restante do código-fonte
COPY . .

WORKDIR /src/MonolitoBackend.Api
RUN dotnet publish "MonolitoBackend.Api.csproj" -c Release -o /app/publish

# Etapa 2: Imagem base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copiar os arquivos publicados
COPY --from=build /app/publish .

# Definir ambiente e porta
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5000

ENTRYPOINT ["dotnet", "MonolitoBackend.Api.dll"]

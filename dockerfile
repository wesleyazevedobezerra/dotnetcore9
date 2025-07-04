# Stage 1: Build com SDK .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

# Copia o arquivo de projeto
COPY todo_dotnet_core9.Api/todo_dotnet_core9.Api.csproj ./todo_dotnet_core9.Api/

# Restaura as dependências
RUN dotnet restore ./todo_dotnet_core9.Api/todo_dotnet_core9.Api.csproj

# Copia todo o código
COPY . ./

# Publica em Release
RUN dotnet publish ./todo_dotnet_core9.Api/todo_dotnet_core9.Api.csproj -c Release -o /app/out

# Stage 2: Runtime leve
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/out ./

# Cria diretório para dados e ajusta permissões (SQLite)
RUN mkdir -p /app/data && chmod 755 /app/data

# (Opcional) Expondo a porta padrão
EXPOSE 8080

# Define o entrypoint
ENTRYPOINT ["dotnet", "todo_dotnet_core9.Api.dll"]

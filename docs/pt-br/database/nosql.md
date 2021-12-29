# Banco de Dados NoSQL
>NoSQL (originalmente se referindo a "no SQL": "não SQL" ou "não relacional", posteriormente estendido para Not Only SQL - Não Somente SQL) é um termo genérico que representa os bancos de dados não relacionais. Uma classe definida de banco de dados que fornecem um mecanismo para armazenamento e recuperação de dados que são modelados de formas diferentes das relações tabulares usadas nos bancos de dados relacionais. [Wikipédia](https://pt.wikipedia.org/wiki/NoSQL)

Foi implementado padrão de repositório com critérios de pesquisa e paginação, além de unidade de trabalho ([Veja Repositório](pt-br/database/use-repository)). Esta implementação não oferece suporte apenas a carga tardia de objetos relacionados. 

## Pré-Requisitos (Não Obrigatório)
Adicione um arquivo de configuração ao projeto com nome "appsettings.json". O arquivo deverá conter um chave com dados de conexão, por exemplo, ConnectionStrings/DataContext conforme abaixo:
```json
{
  "ConnectionStrings": {
    "DataContext": "String de conexão"
  },
  "Mvp24Hours": {
    "Persistence": {
      "MaxQtyByQueryPage": 30
    }
  }
}
```
Você poderá usar a conexão de banco de dados direto, o que não é recomendado. Acesse o site [ConnectionStrings](https://www.connectionstrings.com/) e veja como montar a conexão com seu banco.

## MongoDB
Configuração adicional para MongoDb registrado no "appsettings.json":
```json
{
  "Mvp24Hours": {
    "Persistence": {
      "MongoDb": {
        "EnableTls": false,
        "EnableTransaction": false
      }
    }
  }
}
```

### Instalação
```csharp
/// Package Manager Console >

Install-Package MongoDB.Driver -Version 2.13.2
Install-Package Mvp24Hours.Infrastructure.Data.MongoDb
```
### Configuração
```csharp
/// Startup.cs

services.AddMvp24HoursMongoDb("mycollection", ConfigurationHelper.GetSettings("ConnectionStrings:DataContext"));

```

### Usando Docker
**Comando Básico**
```
// Command
docker run -d --name mongo -p 27017:27017 mvertes/alpine-mongo

// ConnectionString
mongodb://localhost:27017

```

**Comando para Banco com Senha**
```
// Command
docker run --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=user -e MONGO_INITDB_ROOT_PASSWORD=123456 mongo

// ConnectionString
mongodb://user:123456@localhost:27017

```
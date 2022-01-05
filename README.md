# Delivery app
## Web API em .NET 5.0 e SQL Server

## Tecnologias
- .NET 5,0
- SQL Server
- Entity Framework
- Identity Framework
- AutoMapper
- FluentValidation
- Swagger
- Docker e Docker-Compose
- Ardalis Endpoints
- Ardalis Specification
- XUnit
- NSubstitute
- AutoFixture

## Features

- Cadastro de usuários
- Autenticação usando JWT
- CRUD de produtos
- Adicionar, atualizar e limpar carrinho de compras
- Finalizar pedidos
- Consultar pedidos (histórico)

## Build

Clonar o repositório em uma pasta, abrir o cmd/powershell na pasta do projeto e executar o seguinte comando:

```sh
docker-compose up
```
O sistema, por padrão será levantado nas portas 8080 (https) e 8081 (http).

[HTTPS Port 8080 (swagger)](https://localhost:8080/swagger/index.html)
[HTTP Port 8081 (swagger)](http://localhost:8081/swagger/index.html)

O sistema pode ser executado normalmente pelo visual studio também.

## obs
Este sistema foi desenvolvido utilizando a IDE Visual Studio 2019 Enterprise Edition.

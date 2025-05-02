
# MiraeDigital.BffMobile Service- .NET 5

A solução consistem em API para cadastro de novos cliente e geerenciamento de cadastro de clientes existentes.

- MiraeDigital.BffMobile.API
- MiraeDigital.BffMobile.Domain
- MiraeDigital.BffMobile.Infrastructure
- MiraeDigital.BffMobile.UnitTests
- MiraeDigital.BffMobile.IntegrationTests

## MiraeDigital.BffMobile.API

Interface REST/JSON para envio

Essa aplicação também responsável pelo startup dos componentes de Log (Serilog com persitência na AWS Cloud Watch), HealthCheck e Migration da base de dos Postgres

## MiraeDigital.BffMobile.UnitTests

Teste unitário de regras de negócio de cadastro.

## MiraeDigital.BffMobile.IntegrationTests

Projeto de testes de integração do projeto `MiraeDigital.BffMobile.API`

Os testes integrados serão executando a partir do ambiente isolado de containers(BD, Filas e outras API) configurados no no arquivo docker-compose-tests e implemntação a partir da classe WebApplicationFactory<TStartup>.


## Executando a solução:

###Variaáveis de ambiente

Configurações de porta da API , conexões de bases, filas e autenticaçãoe em serviços de externos deverão ser configuradas via variáveis de ambiente assim como as presentes no arquivo .env.dev da solução.


### Via Docker-compose

Executando as aplicações + BD em containers docker

```
docker-compose --env-file .env.dev up --build -d
```

A página do Swagger com a descrição da API estará em

`http://localhost:5010/index.html`


### Modo Debug (Visual Studio)


Será necessário ter o container de banco de dados executando.

```
docker-compose up -d db

```

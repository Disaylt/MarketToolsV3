# MarketToolsV3 (RU)

## Описание проекта

*Данный проект написан на C# и использует ASP.NET.  Цель проекта расширить инструменты автоматизации, аналитики и статистики на маркетплейсах.*

| Тип  | Описание | 
|:---------:| ----------- |
| **Архитектурные решения**    | Microservices, Clear architecture, Event driven architecture, Unit of work & Repositories, Mediator & CQRS, REST, gRPC, EventBus, OAuth (2.0), Cache-Aside Pattern, Api gateway |
| **Библиотеки**    | Serilog, FluentValidation, Google.Protobuf, MassTransit, MediatR, EntityFrameworkCore, DependencyInjection, OpenApi, MongoDB.Driver, Moq, NUnit, Ocelot, Scrutor |
| **Внешние технологии**    | Docker compose, PostgreSql, Mongodb replica set, RabbitMQ, ElasticSearch, Kibana, Redis |

#### Поддерживаемые провайдеры.

| Тип  | Описание | 
|:---------:| ----------- |
| **Маркетплейсы**    | Ozon, Wildberries |
| **Мессенджеры**    | Telegram |
| **Нейросети**    |  |

#### Структура проекта.

* **AppHost(Aspire)**
* **ApiGateway(Ocelot)**
* **EventBus(rabbitMQ)**
* **Identity(PostgreSQL)**
    - Web-API
    - gRPC
* **Notifications(mongodb)**
    - Web-API
    - Processor
* **Wb.Seller.Companies(PostgreSQL)**
    - WebAPI
    - Processor
* **Contracts**
    - IntegrationEvents
    - Protobuf
* **Tests**
    - Identity
* **ConfigurationManager**
* **FakeDataService**

#### Документация
* Деплой
  * Локальное окружение
    * [Как запустить проект в локальном окружении](Deploy/Local/readme.md)
* Configuration
  * [ Настройка менеджера конфигурации ](src/MarketToolsV3.ConfigurationManager/readme.md)

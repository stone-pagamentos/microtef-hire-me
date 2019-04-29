## Technologies implemented:

- ASP.NET Core 2.2 (with .NET Core 2.2)
 - ASP.NET MVC Core 2.
 - ASP.NET WebApi Core 2.2
 - ASP.NET Identity Core 2.2
- Entity Framework Core 2.2
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI

## Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Model Pattern, CQRS and ES concepts
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- CQRS - Command Query Responsibility Segregation
- Event Sourcing
- Unit of Work
- Repository and Generic Repository
- ASP.NET Identity working throught WebAPI services
- Different ways to read and write data
- Unit Tests

## Sobre o desafio:

O desafio proposto pela Stone Payments pode ser acessado aqui -> https://github.com/stone-payments/microtef-hire-me

## Sobre CQRS
CQRS significa Command Query Responsibility Segregation. Como o nome já diz, é sobre separar a responsabilidade de escrita e leitura de seus dados.

CQRS é um pattern, um padrão arquitetural assim como Event Sourcing, Transaction Script e etc. 

O CQRS não é um estilo arquitetural como desenvolvimento em camadas, modelo client-server, REST e etc.

## Onde posso aplicar CQRS
Atualmente as aplicações não são mais para atender 10 usuários simultâneos, a maioria das novas aplicações nascem com
premisas de escalabilidade, performance e disponibilidade, fazer uma aplicação funcionar bem para cargas de trabalho 
de forma elástica é uma tarefa extremamente complexa.

O CQRS prega a divisão de responsabilidade de gravação e escrita de forma conceitual e física. Isto significa que além 
de ter meios separados para gravar e obter um dado os bancos de dados também são diferentes. 

As consultas são feitas de 
forma síncrona em uma base desnormalizada separada e as gravações de forma assíncrona em um banco normalizado.

![Relação cliente-servidor com sonda](image/CQRS_FluxoSimples.jpg)

# Segregar as responsabilidades em QueryStack e CommandStack
A ideia básica é segregar as responsabilidades da aplicação em:

* Command – Operações que modificam o estado dos dados na aplicação.
* Query – Operações que recuperam informações dos dados na aplicação.

**Numa arquitetura de N camadas poderíamos pensar em separar as responsabilidades em CommandStack e QueryStack.**

## Swagger

- See the list of APIs: URL: https://localhost:44338/swagger/index.html

## Generation Database

- Run the scrit /sql/GenerateDataBase.sql


![Relação cliente-servidor com sonda](image/CQRS_BUS.jpg)
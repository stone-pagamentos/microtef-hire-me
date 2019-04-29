## Technologies Implemented:
- ASP.NET Core 2.2 (with .NET Core 2.2)
- ASP.NET MVC Core 2.2
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
- Integration Tests

## Used Software
* Microsft Windows 10 Pro
* Microsoft Visual Studio Professional 2019 (versão 16.0.2)
* Docker Desktop Community Version 2.0.0.3 (31259) - Channel: stable - Build: 8858db3
* Microsoft SQL Server 2017 (RTM-CU13) (KB4466404) - 14.0.3048.4 (X64) Developer Edition 
* SQL Server Management Studio v17.4

## Sobre o Desafio:
O desafio proposto pela Stone Payments pode ser acessado aqui -> https://github.com/stone-payments/microtef-hire-me

Basicamente o desafio consiste na implementação de um sistema que simule o processo de uma transação financeira.

Para simular um sistema de transação financeira foi implementado:
1. **Cliente WPF**
2. **Servidor de comunicações**

### Para resolver o desafio foi necessário criar 4 projetos distintos:
1. **AmonRa** - cliente WPF
2. **EFCoreMapStone** - entity framework, cria o banco de dados, as tabelas, PK´s e FK´s
3. **UnitTesteKarnakStone** - responsável por realizar os testes unitários e de integração
4. **KarnakCore** - o coração do projeto, responsável por tudo, é o cara!

## Funções Disponíveis por Tipo de Operação:
1. **Tipo Transação**
	* Incluir
	* Alterar
	* Excluir
	* Consultar
		* Por nome
		* Por id
	* Listagem
	* Historico
2. **Status Transação**
	* Incluir
	* Alterar
	* Excluir
	* Consultar
		* Por nome
		* Por id
	* Listagem
	* Historico
3. **Bandeira Cartão**
	* Incluir
	* Alterar
	* Excluir
	* Consultar
		* Por nome
		* Por id
	* Listagem
	* Historico
4. **Tipo Cartão**
	* Incluir
	* Alterar
	* Excluir
	* Consultar
		* Por nome
		* Por id
	* Listagem
	* Historico
5. **Clientes**
	* Incluir
	* Alterar
	* Excluir
	* Consultar
		* Por nome
		* Por id
	* Listagem
	* Historico
6. **Cartões**
	* Incluir
	* Alterar
	* Excluir
	* Consultar
		* Por nome
		* Por id
	* Listagem
	* Historico
7. **Transações**
	* Incluir
	* Consultar
		* Por id
	* Sondagem das transações efetuadas
	* Listagem somente das transações efetuadas
	* Listagem das transações com os relacionamentos de dados

## Sobre a Senha
Como em qualquer transação do Mundo real, na solução do desafio proposta não foi diferente. 

Senha sempre é uma questão delicada, para não termos nenhum problema as senhas **sempre são transmitidas de forma criptografada**.

As senhas enviadas do cliente WPF (AmonRa) para o servidor de comunicações (Karnak) são transmitidas de forma criptografada utilizando criptografia de 256 bits.

A verificação da senha fica a cargo do servidor de comunicações (Karnak), o qual verifica se a senha informada é a mesma armazenada no banco de dados.

Todas as senhas armazenadas no banco de dados são criptografadas utilizando criptografia de 256 bits.

Veja abaixo as senhas criptografadas no banco de dados.
![AmonRa - Banco de dados - Senhas Criptografadas](image/senhas_criptografadas_armazenadas_bd.png)

**Os cartões armazenados no banco de dados possuem senha 985471**

## Biblioteca de Terceiros
Para realizar a criptografia e descriptografia da senha foi utilizado a classe **StringCipher**.

## Sobre as Regras de Negócio do Desafio Proposto pela Stone
1. **Cliente WPF (AmonRa)**
	
	*Deve haver duas telas principais:
		
		* **Tela de transação**: input dos dados da transação e envio da transação para o servidor
		
		* **Tela de consulta das transações efetuadas**: lista das transações efetuadas
		
		* **Sondagem das transações**: listagem de todas as operações realizadas referente a um único cartão
	
	* Catálogo de cartões virtuais com o número razoável de cartões para testar diferentes cenários
		
		* **Propriedades básicas**
			* A senha de cada cartão do catálogo deve estar criptografada de algum jeito
			* Com esse catálogo, a verificação da senha do cartão deve ser feita apenas pelo servidor

## Mapemanento ORM
Para atender ao desafio proposto pela Stone foi estruturado um banco de dados com algumas tabelas básicas para a operação do sistema.

O sistema é composto por 7 tabelas, abaixo suas estruturas e seus relacionamentos.

![AmonRa - Banco de dados - Mapeamento ORM](image/banco_de_dados_relacionamentos.png)
Banco de dados - Mapeamento ORM

## Sobre Event Sourcing
Por se tratar de um desafio, no qual são realizadas transações com cartões, visando uma maior segurança e rastreabilidade
optou-se por implmentar o **Event Sourcing**.

A finalidade do Event Sourcing é armazenar no banco de dados histórico de todas as operações recebidas ou enviadas 
pelo servidor de comunicações (Karnak).

# 1 - O Projeto AmonRa - Cliente WPF
O nome AmonRa foi escolhido por se tratar do pai dos Deuses, o senhor da verdade, no antigo egito.

Para atender aos requisitos do desafio, foram criadas telas adicionais:

![AmonRa - Cliente WPF - Tela Transações](image/amonra_cliente_wpf_tela_transacoes.jpg)
Cliente WPF - Tela Transações

![AmonRa - Cliente WPF - Tela Tipo Transação](image/amonra_cliente_wpf_tela_tipo_transacao.jpg)
Cliente WPF - Tela Tipo Transações

![AmonRa - Cliente WPF - Tela Status Transação](image/amonra_cliente_wpf_tela_status_transacao.jpg)
Cliente WPF - Tela Tipo Transações

![AmonRa - Cliente WPF - Tela Bandeira Cartão](image/amonra_cliente_wpf_tela_bandeira_cartao.jpg)
Cliente WPF - Tela Bandeira Cartao

![AmonRa - Cliente WPF - Tela Tipo Cartão](image/amonra_cliente_wpf_tela_tipo_cartao.jpg)
Cliente WPF - Tela Tipo Cartão

![AmonRa - Cliente WPF - Tela Clientes](image/amonra_cliente_wpf_tela_cliente.jpg)
Cliente WPF - Tela Clientes

![AmonRa - Cliente WPF - Tela Cartões](image/amonra_cliente_wpf_tela_cartoes.jpg)
Cliente WPF - Tela Cartões

![AmonRa - Cliente WPF - Tela Listagem Transações](image/amonra_cliente_wpf_tela_listagem_transacoes.jpg)
Cliente WPF - Tela Listagem Transacoes

![AmonRa - Cliente WPF - Tela Sondagem das Transações](image/amonra_cliente_wpf_tela_sondagem_das_transacoes.jpg)
Cliente WPF - Tela Sondagem das Transacoes

![AmonRa - Cliente WPF - Tela Cadastro de Clientes](image/amonra_cliente_wpf_tela_cadastro_cliente.jpg)
Cliente WPF - Tela Cadastro Cliente

O projeto está estruturado da seguinte forma:
1. **Pasta Common**: classes comuns ao projeto
	* Classe StringCipher.cs: responsável por realizar a criptografia e descriptografia da senha
2. **Pasta Core**: responsável por efetuar transações
	* Classe TransactionServer.cs: enviar transações para o servidor Karnak  
3. **Pasta Model**: os modelos de dados		  
	* São as classes que ajudam na realização dos parser´s dos dados que são enviados para o servidor ou que chegam do mesmo
4. **Pasta SendRequestToServer**: enviar requisições para o servidor Karnak 
	* São as classes que realizam conexão diretamente com o api rest do servidor Karnak
5. **Pasta Services**: são os serviços que chamam as classes do passo 4
	* As telas WPF chamam as classes de serviços, que por sua vez chamam as classes do passo 4

### Catálogo de Cartões Virtuais
São os cartões cadastrados no banco de dados, as informações dos cartões são:
1. Número do cartão (cardnumber)
2. Senha do cartão (password), a senha será exigida somente para cartões com chip
3. Valor da transação (amount)
4. Tipo da transação (crédito ou débito)
5. Pacelas, quantidade de parcelas exibida somente para compras do tipo crédito
6. Validade do cartão
7. Bandeira do cartão (visa, master, amex)

### Sondagem das Transações
Para ver as transações realizads por algum cartão, é necessário escolher um cartão na tela da **Listagem Transações**.

### Listagem Transações
São todas as transações realizadas pelo servidor. 
É possível acompanhar o histórico das transações de um determinado cartão.
Todas as transações possuem status de **aprovada** ou **negada**.
 
## Sobre CQRS
CQRS significa Command Query Responsibility Segregation. Como o nome já diz, é sobre separar a responsabilidade de escrita e leitura de seus dados.

CQRS é um pattern, um padrão arquitetural assim como Event Sourcing, Transaction Script e etc. 

O CQRS não é um estilo arquitetural como desenvolvimento em camadas, modelo client-server, REST e etc.

## Onde Posso Aplicar CQRS
Atualmente as aplicações não são mais para atender 10 usuários simultâneos, a maioria das novas aplicações nascem com
premisas de escalabilidade, performance e disponibilidade, fazer uma aplicação funcionar bem para cargas de trabalho 
de forma elástica é uma tarefa extremamente complexa.

O CQRS prega a divisão de responsabilidade de gravação e escrita de forma conceitual e física. 

Isto significa que além de ter meios separados para gravar e obter um dado os bancos de dados também são diferentes. 

As consultas são feitas de forma síncrona em uma base desnormalizada separada e as gravações de forma assíncrona em um banco normalizado.

![Relação cliente-servidor com sonda](image/CQRS_FluxoSimples.jpg)

# Segregar as responsabilidades em QueryStack e CommandStack
A ideia básica é segregar as responsabilidades da aplicação em:

* Command – Operações que modificam o estado dos dados na aplicação.
* Query – Operações que recuperam informações dos dados na aplicação.

**Para resolver o desafio foi utilizado uma arquitetura de N camadas, separarando as responsabilidades em CommandStack e QueryStack.**

## QueryStack
A QueryStack é uma camada síncrona que recupera os dados de um banco de leitura desnormalizado.

## CommandStack
O CommandStack por sua vez é potencialmente assíncrono. 

O CommandStack segue uma abordagem behavior-centric onde toda intenção de negócio é inicialmente disparada pela UI como um caso de uso. 

Utilizamos o conceito de Commands para representar uma intenção de negócio. 

Os Commands são declarados de forma imperativa (ex. Transaction) e são disparados assincronamente no formato de eventos, 
são interpretados pelos CommandHandlers e retornam um evento de sucesso ou falha.

Toda vez que um Command é disparado e altera o estado de uma entidade no banco de gravação um processo tem que ser disparado para 
os agentes que irão atualizar os dados necessários no banco de leitura.

![Relação cliente-servidor com sonda](image/CQRS_BUS.jpg)


## Vantagens de utilizar CQRS
A implementação do CQRS quebra o conceito monolítico clássico de uma implementação de arquitetura em N camadas onde todo o processo 
de escrita e leitura passa pelas mesma camadas e concorre entre si no processamento de regras de negócio e uso de banco de dados.

Este tipo de abordagem aumenta a disponibilidade e escalabilidade da aplicação e a melhoria na performance surge principalmente pelos aspectos:
* Todos comandos são assíncronos e processados em fila, assim diminui-se o tempo de espera.
* Os processos que envolvem regras de negócio existem apenas no sentido da inclusão ou alteração do estado das informações.
* As consultas na QueryStack são feitas de forma separada e independente e não dependem do processamento da CommandStack.
* É possível escalar separadamente os processos da CommandStack e da QueryStack.
 
Uma outra vantagem de utilizar o CQRS é que toda representação do seu domínio será mais expressiva e reforçará a utilização da linguagem ubíqua 
nas intenções de negócio.

Toda a implementação do CQRS pattern pode ser feito manualmente, sendo necessário escrever diversos tipos de classes para cada aspecto, porém 
é possível encontrar alguns frameworks de CQRS que vão facilitar um pouco a implementação e reduzir o tempo de codificação.

Apesar da minha preferência ser sempre codificar tudo por conta própria eu encontrei alguns frameworks bem interessantes que servem inclusive 
para estudo e melhoria do entendimento no assunto.

## Swagger

- See the list of APIs: URL: https://localhost:44338/swagger/index.html

## Generation Database

- Run the scrit /sql/GenerateDataBase.sql



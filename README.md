# EntityFrameworkCore-WebAPI
Um modelo de dados e banco de dados criados com o EntityFrameworkCore e SQL Server.

Esse modelo serve apenas para aplicações usadas com .NET 5. 

Resumo das funções desse Modelo:

- Esse modelo tem uma conexão criada com o SQL Server;

- Todos os modelos/objetos ( Batalhas, Heróis, etc.) foram criados e inseridos no SQL Server através do EntityFramework;

- Esse modelo é separado em camadas : Aplicação, Repositório e Domínio;

- Esse modelo possuí todas as operações básicas (CRUD) para se trabalhar com um banco de dados;

- Possuí também relacionamento de um para um e muitos para muitos entre as entidades inseridas no Banco de dados;

- Criada uma interface para se utilizar com o contexto do Banco e os Controllers.

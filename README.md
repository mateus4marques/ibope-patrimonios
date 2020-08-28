# IBOPE Patrimonios Application

API Rest para gerenciar cadastros de Patrimônios e Marcas. 


### Solucão adotada na constução API 
  Api contruída em .Net Core 3.1 usando o padrão de arquiterura CQRS(Command Query Responsibility Segregation). Tal padrão foi escolhido pela necessidade de aplicar regras distintas em cada operação, ou seja, os comandos(neste caso o Crud) evidenciam melhor a intenção em cima da entidade (Marca e Patrimônio). Para a facilitar implementação do CQRS foi utilizado a biblioteca MediatoR que trás conceitos do CQRS na implementação do design pattern Mediator. Ainda com o MediatR, foi implementado design pattern Event Sourcing  para registrar os eventos gerado após um comando, sendo persistindo no banco de dados em uma tabela de log.
  
  Também foi utilizado o conceito FastFail e Dominios Ricos do DDD. FastFail para validar o valor da propriedade, como por exemplo, quantidade de caracteres. Dominios Ricos para que somente o domínio(entidade) saiba o que tem de fazer, como por exemplo a entidade Patrimônio, somente ela sabe como gerar o Número do Tombo.
  
### Como executar 
- 1- Executar o script localizado em ./sql/v1__init_base.sql em uma instância SQL Server; 
- 2- Alterar a connectionstring localizada ./src/Patrimonios.Api/appsettings.json com as configurações de acesso da sua instância de banco de dados; 
- 3- No terminal, navegar para ./src/Patrimonios.Api/ e rodar o comando: 
   dotnet build | dotnet run
- 4- No navegador acesse https://localhost:5001/swagger tendo acesso a documentação da api, podendo realizar testes 

### Executar testes unitários 
- 1- No terminal, navegar para ./src/Patrimonios.Tests/ e rodar o comando: 
   dotnet build | dotnet test
   
### Referências 
- https://imasters.com.br/back-end/mediator-pattern-com-mediatr-asp-net-core-2-2
- https://docs.microsoft.com/pt-br/azure/architecture/patterns/event-sourcing


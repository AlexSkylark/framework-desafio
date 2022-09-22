# Desafio Framework!

Olá! 
Os arquivos do desafio estão dispostos em 3 pastas:
 - `framework-leads-database`,com os scripts (na verdade script) de banco de dados
 - `framework-leads-server`, com o código-fonte da aplicação back-end (WebAPI sobre .NET Core 6.0)
 - `framework-leads-web`, com a aplicação web de front-end (framework Nuxt.JS sobre Vue.JS + Bulma)
 - `framework-leads-dist`, com versões distribuíveis pre-compiladas dos aplicativos
## Hospedando o projeto
Versões pré-compiladas estão disponíveis na pasta `framework-leads-dist`, em arquivos ZIP contendo a aplicação front-end (em `framework-leads-web.zip`) e a aplicação back-end (em `framework-leads-server`). 

O aplicativo roda sobre uma base de dados SQL server, e como tal depende de um banco de dados pré-configurado para a sua utilização. a operação do aplicativo somente possui uma única tabela, e com o banco de dados preparado, basta executar o script para criá-la, que se encontra-se em `framework-leads-database`. 

### Configurações
A aplicação front-end possui um arquivo de configuração em sua raiz, `.env`, com uma única chave, "BASE_URL", que deve ser configurada apontando para o endereço raiz de onde a aplicação back-end estará hospedada. 

Ao mesmo tempo, a aplicação back-end possui um arquivo em sua raiz, `appsettings.json`, onde a chave "DatabaseConnectionString" deve ser alterada de acordo para apontar para a base de dados que conterá a tabela contendo os dados da aplicação. 
### Dados de Teste
Para a entrada de dados de teste, além da entrada manual dos dados na base, a aplicação servidora .NET provê métodos básicos de CRUD acessíveis diretamente através da API REST `/Leads` (também utilizável através da interface gráfica swagger a partir da URL `<raiz da aplicação>/swagger`).
### Rodando a Aplicação
Com as aplicações de servidor .NET e Web devidamente hospedadas e rodando, o banco de dados configurado e os dados de teste populados, basta acessar a URL raiz da aplicação web, e o SPA Web deverá ser mostrado corretamente.

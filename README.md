# 🎧 DeskFlow API — Gestão de Chamados e Helpdesk de TI

## 🎯 Sobre o Projeto
A **DeskFlow API** é uma Web API RESTful construída em .NET Core 10 utilizando Entity
Framework Core e SQL Server. O sistema automatiza o gerenciamento de chamados de
suporte técnico, histórico de interações e acompanhamento de status do atendimento.

## 🛠️ Tecnologias Utilizadas
- .NET Core 10 / Web API
- Entity Framework Core 10
- SQL Server
- Swagger / OpenAPI
- ASP.NET Core
- Autenticação JWT

## 🚀 Como Executar a Aplicação

### Pré-requisitos
- .NET SDK 10 (ou superior)
- SQL Server em execução (LocalDB, SQL Server Express ou Docker)

### Passo a Passo
1. Clone este repositório:
git clone https://github.com/uokom-9/deskflow-api.git
2. Acesse a pasta do projeto:
cd deskflow-api
3. Configure a Connection String no arquivo `appsettings.json`:
"ConnectionStrings": {
"DefaultConnection":
"Server=localhost;Database=DeskFlowDb;Trusted_Connection=True;TrustServerCertificate=Tr
ue;"
}
4. Execute as Migrations para criar a estrutura no banco de dados:
dotnet ef database update
5. Execute a API:
dotnet run
6. Acesse a documentação do Swagger para testar os endpoints:
http://localhost:5100/swagger

### Como obter e usar o token JWT
1. No Swagger, localize o endpoint de login/autenticação da API.
2. Envie as credenciais de um usuário cadastrado no formato solicitado pelo endpoint. Exemplo:
   ```json
   {
     "email": "admin@deskflow.com",
     "password": "SuaSenha123"
   }
   ```
3. Copie o token JWT retornado na resposta.
4. Clique em **Authorize** no Swagger e informe seu token.

## 🧠 Ciclo de Vida do Chamado
- **Aberto**: Chamado registrado pelo solicitante.
- **EmAndamento**: Suporte em atendimento ao chamado.
- **Fechado**: Chamado encerrado com texto de solução e data de conclusão.

## 🧱 Arquitetura em Camadas
- **Controllers**: Recebem as requisições HTTP e retornam as respostas da API.
- **Services**: Centralizam a regra de negócio e a lógica de status dos chamados.
- **Repositories**: Acessam o banco de dados com Entity Framework Core.
- **Middlewares**: Tratam erros globais e padronizam as respostas.
- **Models / Entities**: Representam as entidades do sistema, como chamados, categorias, usuários e interações.
- **Models / DTOs**: Definem os dados de entrada e saída da API para evitar acoplamento.
- **Data**: Configura o DbContext e a conexão com o SQL Server.
- **script.sql**: Para criação da estrutura do banco de dados.
- **Program.cs**: Inicializa a aplicação, registra serviços e configura a API.

## 📦 Funcionalidades Principais
- Cadastro e autenticação de usuários e técnicos;
- Abertura, andamento e encerramento de chamados;
- Controle de status do atendimento (Aberto, EmAndamento, Fechado);
- Registro de interações por chamado;
- Documentação interativa dos endpoints via Swagger/OpenAPI;

## 🎥 Vídeo de Apresentação
[Vídeo em breve]
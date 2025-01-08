TravelRoute

Descrição

TravelRoute é um sistema para encontrar a rota de viagem mais barata entre dois pontos, considerando possíveis conexões. O sistema suporta dois modos de operação:

Modo Console: Consulta de rotas diretamente pelo terminal.

Modo REST API: Oferece endpoints para consulta de rotas e registro de novas rotas.

Requisitos

.NET 6 ou superior

xUnit para testes unitários

Estrutura do Projeto

projetoGP/
├── TravelRoute/            # Projeto principal
│   ├── Infra/              # Manipulação de arquivos CSV
│   ├── Models/             # Modelos de dados
│   ├── Services/           # Lógica de negócio para rotas
│   ├── Controllers/        # API REST
│   ├── Program.cs          # Configurações de inicialização
│   └── TravelRoute.csproj  # Arquivo de projeto
├── TravelRoute.Tests/      # Projeto de testes unitários
│   ├── Infra/              # Testes do manipulador de CSV
│   ├── Services/           # Testes dos serviços de rotas
│   └── TravelRoute.Tests.csproj

Como Executar o Projeto

Modo REST API

Navegue até o diretório TravelRoute:

cd TravelRoute

Execute o projeto:

dotnet run

O servidor será iniciado e estará disponível em http://localhost:5000.

Endpoints Disponíveis

GET /best-route?origin=GRU&destination=CDG

Retorna a melhor rota entre os pontos informados.

POST /add-route

Adiciona uma nova rota.

Corpo da requisição:

{
  "origin": "GRU",
  "destination": "BRC",
  "cost": 10
}

Modo Console

Navegue até o diretório TravelRoute:

cd TravelRoute

Execute o projeto no modo console:

dotnet run console

Consulte as rotas diretamente pelo terminal:

Digite a rota: GRU-CDG
Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40

Executando os Testes

Navegue até o diretório de testes:

cd TravelRoute.Tests

Execute os testes:

dotnet test

O resultado dos testes será exibido no terminal.

Decisões de Design

Estrutura do Código

Separação de Camadas:

Infra: Responsável pela manipulação de arquivos CSV.

Models: Modelos de dados para representar rotas.

Services: Contém a lógica de negócio (ex: encontrar a melhor rota).

Controllers: Define os endpoints da API REST.

Injeção de Dependência

Os serviços são configurados e injetados automaticamente para garantir flexibilidade e testabilidade.

Testes

xUnit foi usado para garantir a cobertura dos cenários mais importantes:

Testes para encontrar a melhor rota.

Testes para manipulação de arquivos CSV.

Testes para adicionar e recuperar rotas.

Melhorias Futuras

Banco de Dados: Substituir arquivos CSV por um banco de dados relacional ou NoSQL.

Autenticação: Implementar autenticação nos endpoints da API.

Interface Gráfica: Criar uma interface web para facilitar o uso.

Cobertura de Testes: Expandir os testes para cobrir mais cenários de borda.


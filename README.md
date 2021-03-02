# OpenFoodFacts

## Projetos

### OpenFoodFacts.API
### OpenFoodFacts.Application
### OpenFoodFacts.Common
### OpenFoodFacts.Domain
### OpenFoodFacts.Infra
### OpenFoodFacts.Persistence

## Introdução

Api Rest para informação nutricional de diversos produtos alimentícios.

Populando dados a partir da Url https://static.openfoodfacts.org/data/delta/index.txt;

Um serviço é executado a cada dois minutos para buscar os dados.Essa configuração pode ser alterada no arquivo appsettings.json.(Deve ser passado uma expressão Cron);

### Rotas

 - `GET /`: Detalhes da API, informações sobre a leitura e escrita na base de dados, horário da última vez que o CRON foi executado, tempo online e uso de memória.
 - `PUT /products/:code`: Será responsável por receber atualizações do Projeto Web
 - `DELETE /products/:code`: Muda o status do produto para `trash`
 - `GET /products/:code`: Busca produto pelo código informado
 - `GET /products`: Lista todos os produtos da base de dados.
 
 - Observação : Informar no Header de cada requisição a API_KEY. Essa chave pode ser modificada no arquivo appsettings.json.
 

## Como Utilizar

- Após a inicialização do projeto será aberto a documentação da Api na rota /redoc.
- É possível acessar uma dashboard dos serviços executados em background pela rota /jobs.

## Ferramentas utilizadas

-- AutoMapper
-- FluentValidation
-- SqlLite
-- Hangfire
-- EntityFramework
-- Mediator

## Sdk

-- .Net 5

## Conceitos utilizados

-- DDD
-- Solid
-- CQRS (implementado usando apenas um banco, mas fique a vontade para usar outro banco para leitura)

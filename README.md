# Sistema de Gerenciamento de Pedidos

Este projeto é um sistema simplificado de **Gestão de Pedidos** desenvolvido para uma pequena loja, permitindo o cadastro de clientes, produtos e o registro de pedidos, com base em uma arquitetura limpa e boas práticas de desenvolvimento em ASP.NET Core MVC.

## 📌 Tecnologias Utilizadas

- **Backend:** C# com ASP.NET Core MVC
- **Banco de Dados:** SQL Server
- **ORM:** Dapper.NET
- **Frontend:** HTML5, CSS3, Bootstrap 5, jQuery

## 🎯 Funcionalidades

### 1. Gerenciamento de Clientes
- CRUD completo (Criar, Ler, Atualizar, Deletar)
- Filtros por nome ou e-mail
- Campos: `Id`, `Name`, `Email`, `Telephone`, `DateRegister`

### 2. Gerenciamento de Produtos
- CRUD completo
- Filtros por nome
- Campos: `Id`, `Name`, `Description`, `Price`, `StockQuantity`

### 3. Registro de Pedidos
- Seleção de cliente
- Adição de múltiplos produtos com quantidades
- Validação de estoque antes do envio
- Cálculo automático do valor total
- Atualização de status do pedido: `Novo`, `Em processamento`, `Finalizado`
- Exibição de pedidos com filtro por cliente ou status
- Visualização de detalhes do pedido
- Itens do pedido salvos com preço unitário e quantidade no momento da compra

### 4. Interface
- Interface responsiva com Bootstrap
- Filtros dinâmicos e interações via jQuery (ex: adicionar itens ao pedido sem recarregar a página)

## 🧱 Estrutura do Projeto

O projeto está organizado nas seguintes camadas:

```
TManagementOrders
│
├── TManagementOrders.API → Camada de Apresentação (Controllers, Views)
├── TManagementOrders.Application → Camada de Aplicação (Serviços)
├── TManagementOrders.Domain → Camada de Domínio (Entidades, Enums, Entities)
├── TManagementOrders.Infrastructure → Camada de Dados (Repositorios com Dapper.NET)
```


## 💡 Boas Práticas Aplicadas

- Interface base para não repetir codigo C.R.U.D
- Injeção de Dependência com o container nativo do ASP.NET Core
- Padrão Repository aplicado com Dapper.NET
- Organização em camadas (Presentation, Domain, Application, Infrastructure)
- ViewModels para comunicação entre controller e view

## 🛠️ Como Rodar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/TManagementOrders.git
   ```
2. Va até o projeto 
  ```
  TManagementOrders.SQL -> Start_Script ->  SCRIPT_CREATE_DB_TABLES.sql

  Pegue o script e execute em seu banco SQL Server

  Para criar o banco e tabelas base
  ```


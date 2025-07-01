# TodoList Application

## 📋 Descrição
Aplicativo CLI de gerenciamento de tarefas em C# com conexão a banco de dados SQL Server. Permite adicionar, remover e visualizar tarefas de forma simples e intuitiva.

## ✨ Funcionalidades
- `Adicionar Tarefas`: Insere novas tarefas com nome, descrição e tempo estimado
- `Remover Tarefas`: Exclui tarefas existentes por ID
- `Listar Tarefas`: Exibe todas as tarefas cadastradas no banco

## 🛠 Tecnologias
- C# .NET
- Microsoft.Data.SqlClient
- SQL Server

## 🗃 Estrutura do Banco

```
CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255),
    TempoEstimado NVARCHAR(50)
);
```

## 📝 Classe Tasks

```
  internal class Tasks {
     public int Id { get; set; }
     public string Name { get; set; }
     public string Description { get; set; }
     public string EstimatedTime { get; set; }
 }
```

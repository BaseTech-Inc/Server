<img src="https://github.githubassets.com/images/mona-whisper.gif" />

## Tecnologias

- [ASP.NET Core 5]( https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five)
- [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)

## Começando

### User-secrets configuração

Para inicializar os valores secretos é nescessário executar o seguinte comando em **WebAPI/**:

```bash
    dotnet user-secrets init
```

E é nescessário  colocar os seguintes valores:

```bash
    # A SecretKey é usada para gerar o token de acesso
    dotnet user-secrets set "JWT:SecretKeyy" "valor_aleatório"
```

Para gerar um valor aleatório pode-se usar esse [site](https://www.uuidgenerator.net/).

### Banco de dados configuração

Se desejar usar o SQL Server, precisará atualizar **WebAPI/appsettings.json** da seguinte maneira:

```json
  "UseInMemoryDatabase": false,
```

Verifique se a string de conexão DefaultConnection em **WebAPI/appsettings.json** 
aponta para uma instância válida do SQL Server.

### Banco de dados migrations

Para criar as migrações é nescessário navegar até **Infrastructure/**, abrir o terminal 
e executar o seguinte comando:

```bash
    dotnet ef migrations add [MigrationName] -s ../WebAPI/ -o ./Persistence/Migrations/
```

Para aplicar as migrações no banco de dados é nescessário executar o seguinte comando:

```bash
    dotnet ef database update -s ../WebAPI/
```

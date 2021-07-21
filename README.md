<img src="https://github.githubassets.com/images/mona-whisper.gif" />

## Tecnologias

- [ASP.NET Core 5]( https://docs.microsoft.com/pt-br/dotnet/core/dotnet-five)
- [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)

## Come�ando

### User-secrets configura��o

Para inicializar os valores secretos � nescess�rio executar o seguinte comando em **WebAPI/**:

```bash
    dotnet user-secrets init
```

E � nescess�rio  colocar os seguintes valores:

```bash
    # A SecretKey � usada para gerar o token de acesso
    dotnet user-secrets set "JWT:SecretKeyy" "valor_aleat�rio"
```

Para gerar um valor aleat�rio pode-se usar esse [site](https://www.uuidgenerator.net/).

### Banco de dados configura��o

Se desejar usar o SQL Server, precisar� atualizar **WebAPI/appsettings.json** da seguinte maneira:

```json
  "UseInMemoryDatabase": false,
```

Verifique se a string de conex�o DefaultConnection em **WebAPI/appsettings.json** 
aponta para uma inst�ncia v�lida do SQL Server.

### Banco de dados migrations

Para criar as migra��es � nescess�rio navegar at� **Infrastructure/**, abrir o terminal 
e executar o seguinte comando:

```bash
    dotnet ef migrations add [MigrationName] -s ../WebAPI/ -o ./Persistence/Migrations/
```

Para aplicar as migra��es no banco de dados � nescess�rio executar o seguinte comando:

```bash
    dotnet ef database update -s ../WebAPI/
```

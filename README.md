<center>

:umbrella: Tupã Server - API usada para a comunicação entre os serviços e produtos da empresa Tupã - [Mobile](https://github.com/BaseTech-Inc/Tupa-Mobile), [Web](https://github.com/BaseTech-Inc/Tupa-Web) e [Desktop](https://github.com/BaseTech-Inc/Tupa-Desktop).

</center>

<img src="https://github.githubassets.com/images/mona-whisper.gif" align="right" />

## Começando

### Instalando localmente projeto

```bash
# Clone o repositório em sua máquina
$ git clone https://github.com/BaseTech-Inc/Tupa-Server.git
```

### Configurando ambiente

- Para inicializar os [valores secretos](https://docs.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows) 
  é nescessário executar o seguinte comando em **`WebAPI/`**:

    ```bash
        $ dotnet user-secrets init
    ```

    É nescessário inserir os seguintes valores:

    ```bash
        # A SecretKey é usada para gerar o token de acesso
        $ dotnet user-secrets set "JWT:SecretKeyy" "valor_aleatório"
    ```

    Para gerar um valor aleatório pode-se usar esse [site](https://www.uuidgenerator.net/).

- Se desejar usar o SQL Server, precisará atualizar **`WebAPI/appsettings.json`** da seguinte maneira:

    ```json
      "UseInMemoryDatabase": false,
    ```

    Verifique se a string de conexão DefaultConnection em **`WebAPI/appsettings.json`** 
    aponta para uma instância válida do SQL Server.

- Para criar as [migrações](https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) é nescessário navegar até **`Infrastructure/`**, abrir o terminal 
e executar o seguinte comando:

    ```bash
        $ dotnet ef migrations add "MigrationName" -s ../WebAPI/ -o ./Persistence/Migrations/
    ```

    Para aplicar as migrações no banco de dados é nescessário executar o seguinte comando:

    ```bash
        $ dotnet ef database update -s ../WebAPI/
    ```

## Arquitetura

Como esse software funciona internamente e como ele interage com dependências 
externas - escritos em detalhes em [ARCHITECTURE.md](ARCHITECTURE.md).
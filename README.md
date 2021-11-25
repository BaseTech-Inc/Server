<div align="center">
<img align="center" width="24%" src="./.github/logo.png" />
<br>
<i>
    API usada para a comunicação entre os serviços e produtos da empresa Tupã - Mobile, Web e Desktop.
</i>
</div>

## Ajustes e melhorias

O projeto ainda está em desenvolvimento e as próximas atualizações serão voltadas nas seguintes tarefas:

- [ ] Authentication
	- [ ] Autenticação usando Facebook

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

        # O ClientId é usada para validar o token da Google
        $ dotnet user-secrets set "Authentication:Google:ClientId" "ClientId"

        # O username é usado para validar o e-mail smtp
        $ dotnet user-secrets set "smtp:username" "username"

        # O password é usado para validar o e-mail smtp
        $ dotnet user-secrets set "smtp:password" "password"

        # O fromAdress é usado para ser o rementente do e-mail
        $ dotnet user-secrets set "smtp:fromAdress" "fromAdress"

        $ dotnet user-secrets set "smtp:host" "host"

        # O appid é para fazer requisições na API OpenWeather
        $ dotnet user-secrets set "OpenWeather:appid" "appid"
    ```

    Para gerar um valor aleatório pode-se usar esse [site](https://www.uuidgenerator.net/), e para usar a autenticação pelo Google pode usar esse [site](https://developers.google.com/workspace/guides/getstarted-overview).

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

### Usando o  projeto

- Para usar o projeto localmente é nescessário [Definir como projeto de inicialização](https://docs.microsoft.com/pt-br/visualstudio/ide/creating-solutions-and-projects?view=vs-2019) o projeto `WebAPI`.

## Arquitetura

Como esse software funciona internamente e como ele interage com dependências 
externas - escritos em detalhes em [`ARCHITECTURE.md`](ARCHITECTURE.md).

<img src="https://github.githubassets.com/images/mona-whisper.gif" align="right" />

## Licença

Esse projeto está sob licença. Veja o arquivo [`LICENÇA`](https://github.com/BaseTech-Inc/Tupa-Server/blob/master/LICENSE) para mais detalhes.
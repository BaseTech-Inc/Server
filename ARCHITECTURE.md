# Arquitetura

- [Arquitetura](#Arquitetura)
  - [`src/`](#Sources)
    - [Vis�o geral](#Vis�o-geral)
    - [`Application/` - L�gica de neg�cios](#Application---L�gica-do-aplicativo)
    - [`Domain/` - Entidade](#Domain---Entidade)
    - [`Infrastructure/` - Conex�es externas](#Infrastructure---Conex�es-externas)
    - [`WebAPI/`](#WebAPI)

## Sources

### Vis�o Geral
Aqui cont�m todos os arquivos e projetos, onde a intera��o acontecer�.
Esse projeto � desenvolvido seguindos os princ�pios da **Arquitetura Limpa (Clean Architecture)**.

<img width="40%" src="./.github/DiagramaCleanArchitecture.png" align="right"  />

O diagrama d� a vis�o geral das refer�ncias e intera��es de cada camada que d� forma a este software.

### Application - L�gica do aplicativo
Esta camada cont�m toda a l�gica do aplicativo. Esta camada define interfaces que s�o implementadas 
por camadas externas.

**Regras sobre domains responsabilidades de cada estrutura:**

- Depende da camada de dom�nio, mas n�o depende de nenhuma outra camada ou projeto.

**Refer�ncias**
- [`Domain/`](#Domain---Entidade)

### Domain - Entidade
Esta camada conter� todas as entidades, enumera��es, tipos e � onde toda a l�gica de neg�cios deve 
estar contida.

**Regras sobre domains responsabilidades de cada estrutura:**

- Ele nunca deve interagir com qualquer camada que n�o seja suas pr�prias subpastas;
- Ele nunca deve acessar nenhuma outra classe de camada (nem mesmo indiretamente).

**Refer�ncias**
- Nenhuma

### Infrastructure - Conex�es externas
Comunica-se com bibliotecas e estruturas para acessar recusos externos. Essas classes devem ser 
baseadas em interfaces definidas na camada de aplicativo.

**Refer�ncias**
- [`Domain/`](#Domain---Entidade)
- [`Application/`](#Application---L�gica-do-aplicativo)

### WebAPI
Http resquest e response.

Esta camada � um aplicativo Web API baseado em ASP.NET Core 5. 

**Regras sobre domains responsabilidades de cada estrutura:**

- Infraestrutura serve apenas para oferecer suporte � inje��o de depend�ncia;

**Refer�ncias**
- [`Application/`](#Application---L�gica-de-neg�cios)
- [`Infrastructure/`](#Infrastructure---Conex�es-externas)
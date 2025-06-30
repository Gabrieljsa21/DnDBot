# DnDBot

Aplica��o bot para automa��o e suporte a jogos de RPG de mesa, com foco em Dungeons & Dragons 5� Edi��o, integrada ao Discord.

Desenvolvido com arquitetura modular e pr�ticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gest�o de rolagens de dados, cria��o e visualiza��o de fichas de personagem, al�m de suporte para eventos e comandos customizados dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extens�vel para cria��o de ferramentas customizadas para campanhas digitais, otimizando a experi�ncia dos jogadores e mestres por meio da automa��o de tarefas repetitivas e complexas.

---

## Funcionalidades Principais

- Comando slash `/roll` para rolar dados no formato NdX+Y (exemplo: 2d6+3), com suporte para modificadores.
- Sistema completo para cria��o, edi��o e visualiza��o de fichas de personagem.
- Intera��o via modais e menus suspensos para escolha din�mica de:
  - Ra�a (com suporte a sub-ra�as),
  - Classe,
  - Antecedente,
  - Alinhamento.
- Valida��o autom�tica das fichas para garantir integridade dos dados antes da confirma��o.
- Listagem r�pida e embutida das fichas criadas por cada jogador.
- Servi�os especializados para gerenciamento de ra�as, classes, antecedentes e alinhamentos.
- Estrutura modular para f�cil extens�o e manuten��o do c�digo.

---

## Estrutura do Projeto

O projeto est� dividido em camadas e pastas que facilitam a organiza��o, manuten��o e expans�o futura:

- **DnDBot.Application**  
  Cont�m toda a l�gica de neg�cio e servi�os relacionados ao jogo:
  - Servi�os para gerenciamento de fichas, ra�as, classes, antecedentes e alinhamentos.
  - Modelos de dados que representam as entidades do jogo (ex: `FichaPersonagem`, `Raca`).
  - Respons�vel pela manipula��o e persist�ncia b�sica em mem�ria ou arquivo.

- **DnDBot.Bot**  
  Cont�m a l�gica de integra��o com o Discord:
  - Comandos slash, handlers de modais e intera��es.
  - Uso da biblioteca Discord.Net para cria��o e gest�o das intera��es do bot.
  - Classes que respondem a eventos, coletam dados dos usu�rios e executam a l�gica de aplica��o.
  
- **Data**  
  Pasta onde ficam os arquivos JSON com dados est�ticos:
  - Exemplo: `racas.json` que armazena as ra�as e sub-ra�as, para f�cil manuten��o e expans�o sem precisar recompilar.

- **Outros projetos (Domain, Infrastructure, Shared)**  
  Preparados para poss�veis expans�es do sistema, como persist�ncia em banco de dados, integra��o com APIs externas, etc.

---

## Servi�os Detalhados

### RacasService

- Carrega o arquivo `Data/racas.json` para popular uma lista de ra�as com nome, descri��o e sub-ra�as.
- Fornece m�todos para obter a lista completa de ra�as para uso nos selects do Discord.
- Permite futura expans�o para carregar ou modificar dinamicamente as ra�as.

### ClassesService

- Mant�m uma lista est�tica das classes dispon�veis no jogo (exemplo: B�rbaro, Mago, Guerreiro).
- Fornece m�todo para obter a lista, usada no select do Discord.

### AntecedentesService

- Lista fixa dos antecedentes (backgrounds) usados no jogo.
- M�todo para obten��o e uso din�mico no select.

### AlinhamentosService

- Lista os nove alinhamentos cl�ssicos do D&D, facilitando a escolha durante a cria��o da ficha.

### FichaService

- Respons�vel por armazenar, recuperar e listar as fichas dos jogadores.
- Controla a persist�ncia em mem�ria (pode ser adaptado para banco de dados futuramente).
- Garante que cada jogador possa ter m�ltiplas fichas e que elas sejam recuperadas facilmente.

---

## Fluxo Completo da Cria��o da Ficha

1. **Comando `/ficha_criar`**  
   O bot abre um modal solicitando o nome do personagem.

2. **Modal preenchido pelo usu�rio**  
   O nome � validado; se inv�lido, o bot responde com erro.

3. **Montagem dos selects**  
   Depois do nome v�lido, o bot envia menus suspensos para escolha de Ra�a, Classe, Antecedente e Alinhamento, carregando os dados dinamicamente dos servi�os.

4. **Escolha dos detalhes**  
   Cada escolha feita pelo usu�rio atualiza uma ficha tempor�ria armazenada em mem�ria.

5. **Valida��o final**  
   Quando todas as op��es estiverem selecionadas e v�lidas, a ficha � salva permanentemente e o bot confirma a cria��o com um resumo dos dados.

6. **Comando `/ficha_ver`**  
   O usu�rio pode visualizar todas suas fichas criadas, apresentadas via embed no Discord, facilitando a consulta r�pida.

---

## Modelos Principais

### FichaPersonagem.cs

```csharp
public class FichaPersonagem
{
    public ulong JogadorId { get; set; }
    public string Nome { get; set; }
    public string Raca { get; set; }
    public string Classe { get; set; }
    public string Antecedente { get; set; }
    public string Alinhamento { get; set; }
}

### Raca.cs

```csharp
public class Raca
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public List<string> SubRacas { get; set; }
}

### Exemplo JSON para Ra�as (Data/racas.json)

```json
[
  {
    "Nome": "An�o",
    "Descricao": "Robusto e resistente.",
    "SubRacas": ["An�o da Colina", "An�o da Montanha"]
  },
  {
    "Nome": "Elfo",
    "Descricao": "�gil e de sentidos agu�ados.",
    "SubRacas": ["Alto Elfo", "Elfo da Floresta"]
  }
]

## Como Rodar
Configure a vari�vel de ambiente DISCORD_TOKEN com o token do seu bot Discord.

Compile e execute o projeto DnDBot.Bot.

No servidor Discord, utilize os comandos:

/roll para rolar dados.

/ficha_criar para criar uma ficha de personagem.

/ficha_ver para visualizar suas fichas.


## Exemplos de Uso

Usu�rio: /ficha_criar
Bot: Modal abre pedindo nome do personagem.
Usu�rio: Zephyr
Bot: Mostra selects para ra�a, classe, antecedente e alinhamento.
Usu�rio: Seleciona as op��es.
Bot: ? Ficha do personagem criada com sucesso!
Nome: Zephyr  
Ra�a: Elfo  
Classe: Mago  
Antecedente: S�bio  
Alinhamento: Neutro e Bom  


## Como Contribuir
Abra issues para reportar bugs ou sugerir melhorias.

Envie pull requests para adicionar funcionalidades ou corrigir problemas.

Atualize o arquivo JSON Data/racas.json para modificar ou incluir novas ra�as.

Expanda os servi�os para Classes, Antecedentes e Alinhamentos conforme necessidade.

Mantenha o padr�o de modularidade e separa��o de responsabilidades.

Estrutura do Projeto
/DnDBot
 +- /Bot
 �    +- Commands
 �    �    +- ComandosFichaPersonagem.cs
 �    �    +- Outros comandos...
 �    +- ...
 +- /Application
 �    +- Services
 �    �    +- RacasService.cs
 �    �    +- ClassesService.cs
 �    �    +- AntecedentesService.cs
 �    �    +- AlinhamentosService.cs
 �    �    +- FichaService.cs
 �    +- Models
 �    �    +- FichaPersonagem.cs
 �    �    +- Raca.cs
 �    +- ...
 +- /Data
 �    +- racas.json
 +- Program.cs

# DnDBot

Aplica��o bot para automa��o e suporte a jogos de RPG de mesa, com foco em **Dungeons & Dragons 5� Edi��o**, integrada ao Discord.

Desenvolvido com arquitetura modular e pr�ticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gest�o de rolagens de dados, cria��o e visualiza��o de fichas de personagem, al�m de suporte para eventos e comandos customizados dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extens�vel para cria��o de ferramentas customizadas para campanhas digitais, otimizando a experi�ncia dos jogadores e mestres por meio da automa��o de tarefas repetitivas e complexas.

---

## Funcionalidades Principais

- Comando slash `/roll` para rolar uma ou mais combina��es de dados no formato `NdX+Y` (ex: `2d6+2`, `1d10+5`), com suporte a m�ltiplas express�es e modificadores.
- Comandos `/roll_vantagem` e `/roll_desvantagem` que rolam dois dados e escolhem o maior ou menor valor, respectivamente, para situa��es de vantagem ou desvantagem.
- Sistema completo para cria��o, edi��o, distribui��o de atributos e visualiza��o de fichas de personagem.
- Distribui��o de atributos interativa com sistema **point-buy** e valida��es autom�ticas.
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
  - Servi�os para gerenciamento de fichas, ra�as, classes, antecedentes, alinhamentos e distribui��o de atributos.
  - Modelos de dados que representam as entidades do jogo (ex: `FichaPersonagem`, `Raca`, `DistribuicaoAtributosTemp`).
  - Respons�vel pela manipula��o e persist�ncia b�sica em mem�ria ou arquivo.

- **DnDBot.Bot**  
  Cont�m a l�gica de integra��o com o Discord:
  - Comandos slash, handlers de modais e intera��es.
  - Uso da biblioteca Discord.Net para cria��o e gest�o das intera��es do bot.
  - Classes que respondem a eventos, coletam dados dos usu�rios e executam a l�gica de aplica��o.

- **Data**  
  Pasta onde ficam os arquivos JSON com dados est�ticos:
  - Exemplo: `racas.json` que armazena as ra�as e sub-ra�as com todos seus atributos.

- **Outros projetos (Domain, Infrastructure, Shared)**  
  Preparados para poss�veis expans�es do sistema, como persist�ncia em banco de dados, integra��o com APIs externas, etc.

---

## Banco de Dados e Persist�ncia

A partir da vers�o `1.2.0`, o DnDBot utiliza **SQLite** para persist�ncia de dados, permitindo salvar fichas e entidades do jogo de forma dur�vel.

- A base de dados `dndbot.db` � criada automaticamente na raiz do projeto.
- As entidades principais (`FichaPersonagem`, `Tesouro`, `Proficiencia`, etc.) s�o mapeadas em tabelas SQLite com relacionamentos.
- As listas da ficha (como profici�ncias, idiomas e magias raciais) s�o armazenadas em tabelas auxiliares para representar relacionamentos muitos-para-muitos.

### Estrutura de Tabelas

- `FichaPersonagem`: dados principais da ficha.
- `Tesouro`: moedas e bens do personagem.
- Tabelas auxiliares:
  - `Ficha_Proficiencia`
  - `Ficha_Idioma`
  - `Ficha_Resistencia`
  - `Ficha_Caracteristica`
  - `Ficha_MagiaRacial`
  - `HistoricoFinanceiro`

### Cat�logos

- `Raca`, `SubRaca`, `Classe`, `Antecedente`, `Alinhamento`, `Proficiencia`, `Idioma`, `Resistencia`, `Caracteristica`, `Magia`.

Essas tabelas s�o populadas a partir de arquivos JSON em `/Data`, permitindo manuten��o e expans�o din�mica dos dados do sistema.

> Voc� pode explorar a base usando [DB Browser for SQLite](https://sqlitebrowser.org/).

---

## Fluxo Completo da Cria��o da Ficha

1. **Comando `/ficha_criar`**  
   O bot abre um modal solicitando o nome do personagem.

2. **Modal preenchido pelo usu�rio**  
   O nome � validado; se inv�lido, o bot responde com erro.

3. **Montagem dos selects**  
   Ap�s o nome v�lido, o bot envia menus suspensos para escolha de Ra�a, Classe, Antecedente e Alinhamento, carregando os dados dinamicamente dos servi�os.

4. **Escolha dos detalhes**  
   Cada escolha feita pelo usu�rio atualiza uma ficha tempor�ria armazenada em mem�ria.

5. **Distribui��o de Atributos**  
   Comando `/ficha_atributos` permite distribuir os atributos em um sistema point-buy interativo, com bot�es para aumentar/diminuir os valores e valida��o autom�tica.

6. **Confirma��o Final**  
   Quando todos os campos e atributos forem definidos, o bot salva a ficha e confirma a cria��o.

7. **Comando `/ficha_ver`**  
   O usu�rio pode visualizar todas suas fichas criadas, apresentadas via embed no Discord, facilitando a consulta r�pida.

---

## Modelos Principais

### FichaPersonagem.cs

```csharp
public class FichaPersonagem
{
    public Guid Id { get; set; }
    public ulong IdJogador { get; set; }
    public string Nome { get; set; }
    public string IdRaca { get; set; }
    public string IdSubraca { get; set; }
    public string IdClasse { get; set; }
    public string IdAntecedente { get; set; }
    public string IdAlinhamento { get; set; }
    public int Forca { get; set; }
    public int Destreza { get; set; }
    public int Constituicao { get; set; }
    public int Inteligencia { get; set; }
    public int Sabedoria { get; set; }
    public int Carisma { get; set; }
    public List<BonusAtributo> BonusAtributos { get; set; }
    public List<string> IdProficiencias { get; set; }
    public List<string> IdIdiomas { get; set; }
    public List<string> IdResistencias { get; set; }
    public List<string> IdCaracteristicas { get; set; }
    public List<string> IdMagiasRaciais { get; set; }
    public string Tamanho { get; set; }
    public int Deslocamento { get; set; }
    public int? VisaoNoEscuro { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public bool EstaAtivo { get; set; }
    public Tesouro Tesouro { get; set; }
    public List<string> HistoricoFinanceiro { get; set; }
}

### Raca.cs

```csharp
public class Raca
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public List<SubRaca> SubRacas { get; set; }
}

### SubRaca.cs

```csharp
public class SubRaca
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Dictionary<string, int> BonusAtributos { get; set; }
    public string Tendencia { get; set; }
    public string Tamanho { get; set; }
    public string Deslocamento { get; set; }
    public List<string> Idiomas { get; set; }
    public List<string> Proficiencias { get; set; }
    public string VisaoNoturna { get; set; }
    public string Resistencia { get; set; }
    public List<string> Caracteristicas { get; set; }
    public List<string> MagiasRaciais { get; set; }
    public string IconeUrl { get; set; }
    public string ImagemUrl { get; set; }
}

## Exemplos de Uso
Usu�rio: /ficha_criar
Bot: Modal abre pedindo nome do personagem.
Usu�rio: Zephyr
Bot: Mostra selects para ra�a, classe, antecedente e alinhamento.
Usu�rio: Seleciona as op��es.
Bot: Envia interface interativa de distribui��o de atributos.
Usu�rio: Distribui os pontos com os bot�es + e -.
Bot: ? Ficha do personagem criada com sucesso!

## Como Contribuir
Abra issues para reportar bugs ou sugerir melhorias.

Envie pull requests para adicionar funcionalidades ou corrigir problemas.

Atualize os arquivos JSON em /Data para modificar ou incluir novas ra�as e sub-ra�as.

Expanda os servi�os para suportar novas mec�nicas do D&D.

Mantenha o padr�o de modularidade e separa��o de responsabilidades.

## Configurando e Atualizando o Banco de Dados com Migra��es

Para criar ou atualizar o banco de dados utilizando migra��es do Entity Framework Core:

Abra o terminal ou prompt de comando.

Navegue at� a pasta raiz do projeto:
cd D:\source\repos\DnDBot
Para criar uma nova migra��o (exemplo: "InitialCreate"):
dotnet ef migrations add InitialCreate
Para aplicar as migra��es e atualizar o banco de dados:
dotnet ef database update
Caso execute os comandos de uma pasta diferente, especifique o caminho do projeto com a flag --project:
dotnet ef migrations add Inicial_00 --project DnDBot.Application --startup-project DnDBot.Bot
dotnet ef database update --project DnDBot.Application --startup-project DnDBot.Bot

Passo a passo para gerar um token novo para seu bot no Discord:
Acesse o Discord Developer Portal:
https://discord.com/developers/applications

Selecione sua aplica��o/bot na lista.

No menu lateral, clique em �Bot�.

Na se��o �Token�, clique em �Reset Token� ou �Regenerate�.
Isso vai invalidar o token antigo e gerar um novo.

Copie o novo token gerado.

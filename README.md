# DnDBot

Aplicação bot para automação e suporte a jogos de RPG de mesa, com foco em **Dungeons & Dragons 5ª Edição**, integrada ao Discord.

Desenvolvido com arquitetura modular e práticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gestão de rolagens de dados, criação e visualização de fichas de personagem, além de suporte para eventos e comandos customizados dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extensível para criação de ferramentas customizadas para campanhas digitais, otimizando a experiência dos jogadores e mestres por meio da automação de tarefas repetitivas e complexas.

---

## Funcionalidades Principais

- Comando slash `/roll` para rolar uma ou mais combinações de dados no formato `NdX+Y` (ex: `2d6+2`, `1d10+5`), com suporte a múltiplas expressões e modificadores.
- Comandos `/roll_vantagem` e `/roll_desvantagem` que rolam dois dados e escolhem o maior ou menor valor, respectivamente, para situações de vantagem ou desvantagem.
- Sistema completo para criação, edição, distribuição de atributos e visualização de fichas de personagem.
- Distribuição de atributos interativa com sistema **point-buy** e validações automáticas.
- Interação via modais e menus suspensos para escolha dinâmica de:
  - Raça (com suporte a sub-raças),
  - Classe,
  - Antecedente,
  - Alinhamento.
- Validação automática das fichas para garantir integridade dos dados antes da confirmação.
- Listagem rápida e embutida das fichas criadas por cada jogador.
- Serviços especializados para gerenciamento de raças, classes, antecedentes e alinhamentos.
- Estrutura modular para fácil extensão e manutenção do código.

---

## Estrutura do Projeto

O projeto está dividido em camadas e pastas que facilitam a organização, manutenção e expansão futura:

- **DnDBot.Application**  
  Contém toda a lógica de negócio e serviços relacionados ao jogo:
  - Serviços para gerenciamento de fichas, raças, classes, antecedentes, alinhamentos e distribuição de atributos.
  - Modelos de dados que representam as entidades do jogo (ex: `FichaPersonagem`, `Raca`, `DistribuicaoAtributosTemp`).
  - Responsável pela manipulação e persistência básica em memória ou arquivo.

- **DnDBot.Bot**  
  Contém a lógica de integração com o Discord:
  - Comandos slash, handlers de modais e interações.
  - Uso da biblioteca Discord.Net para criação e gestão das interações do bot.
  - Classes que respondem a eventos, coletam dados dos usuários e executam a lógica de aplicação.

- **Data**  
  Pasta onde ficam os arquivos JSON com dados estáticos:
  - Exemplo: `racas.json` que armazena as raças e sub-raças com todos seus atributos.

- **Outros projetos (Domain, Infrastructure, Shared)**  
  Preparados para possíveis expansões do sistema, como persistência em banco de dados, integração com APIs externas, etc.

---

## Banco de Dados e Persistência

A partir da versão `1.2.0`, o DnDBot utiliza **SQLite** para persistência de dados, permitindo salvar fichas e entidades do jogo de forma durável.

- A base de dados `dndbot.db` é criada automaticamente na raiz do projeto.
- As entidades principais (`FichaPersonagem`, `Tesouro`, `Proficiencia`, etc.) são mapeadas em tabelas SQLite com relacionamentos.
- As listas da ficha (como proficiências, idiomas e magias raciais) são armazenadas em tabelas auxiliares para representar relacionamentos muitos-para-muitos.

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

### Catálogos

- `Raca`, `SubRaca`, `Classe`, `Antecedente`, `Alinhamento`, `Proficiencia`, `Idioma`, `Resistencia`, `Caracteristica`, `Magia`.

Essas tabelas são populadas a partir de arquivos JSON em `/Data`, permitindo manutenção e expansão dinâmica dos dados do sistema.

> Você pode explorar a base usando [DB Browser for SQLite](https://sqlitebrowser.org/).

---

## Fluxo Completo da Criação da Ficha

1. **Comando `/ficha_criar`**  
   O bot abre um modal solicitando o nome do personagem.

2. **Modal preenchido pelo usuário**  
   O nome é validado; se inválido, o bot responde com erro.

3. **Montagem dos selects**  
   Após o nome válido, o bot envia menus suspensos para escolha de Raça, Classe, Antecedente e Alinhamento, carregando os dados dinamicamente dos serviços.

4. **Escolha dos detalhes**  
   Cada escolha feita pelo usuário atualiza uma ficha temporária armazenada em memória.

5. **Distribuição de Atributos**  
   Comando `/ficha_atributos` permite distribuir os atributos em um sistema point-buy interativo, com botões para aumentar/diminuir os valores e validação automática.

6. **Confirmação Final**  
   Quando todos os campos e atributos forem definidos, o bot salva a ficha e confirma a criação.

7. **Comando `/ficha_ver`**  
   O usuário pode visualizar todas suas fichas criadas, apresentadas via embed no Discord, facilitando a consulta rápida.

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
Usuário: /ficha_criar
Bot: Modal abre pedindo nome do personagem.
Usuário: Zephyr
Bot: Mostra selects para raça, classe, antecedente e alinhamento.
Usuário: Seleciona as opções.
Bot: Envia interface interativa de distribuição de atributos.
Usuário: Distribui os pontos com os botões + e -.
Bot: ? Ficha do personagem criada com sucesso!

## Como Contribuir
Abra issues para reportar bugs ou sugerir melhorias.

Envie pull requests para adicionar funcionalidades ou corrigir problemas.

Atualize os arquivos JSON em /Data para modificar ou incluir novas raças e sub-raças.

Expanda os serviços para suportar novas mecânicas do D&D.

Mantenha o padrão de modularidade e separação de responsabilidades.

## Configurando e Atualizando o Banco de Dados com Migrações

Para criar ou atualizar o banco de dados utilizando migrações do Entity Framework Core:

Abra o terminal ou prompt de comando.

Navegue até a pasta raiz do projeto:
cd D:\source\repos\DnDBot
Para criar uma nova migração (exemplo: "InitialCreate"):
dotnet ef migrations add InitialCreate
Para aplicar as migrações e atualizar o banco de dados:
dotnet ef database update
Caso execute os comandos de uma pasta diferente, especifique o caminho do projeto com a flag --project:
dotnet ef migrations add Inicial_00 --project DnDBot.Application --startup-project DnDBot.Bot
dotnet ef database update --project DnDBot.Application --startup-project DnDBot.Bot

Passo a passo para gerar um token novo para seu bot no Discord:
Acesse o Discord Developer Portal:
https://discord.com/developers/applications

Selecione sua aplicação/bot na lista.

No menu lateral, clique em “Bot”.

Na seção “Token”, clique em “Reset Token” ou “Regenerate”.
Isso vai invalidar o token antigo e gerar um novo.

Copie o novo token gerado.

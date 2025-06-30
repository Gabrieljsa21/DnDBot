# DnDBot

Aplicação bot para automação e suporte a jogos de RPG de mesa, com foco em Dungeons & Dragons 5ª Edição, integrada ao Discord.

Desenvolvido com arquitetura modular e práticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gestão de rolagens de dados, criação e visualização de fichas de personagem, além de suporte para eventos e comandos customizados dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extensível para criação de ferramentas customizadas para campanhas digitais, otimizando a experiência dos jogadores e mestres por meio da automação de tarefas repetitivas e complexas.

---

## Funcionalidades Principais

- Comando slash `/roll` para rolar dados no formato NdX+Y (exemplo: 2d6+3), com suporte para modificadores.
- Sistema completo para criação, edição e visualização de fichas de personagem.
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
  - Serviços para gerenciamento de fichas, raças, classes, antecedentes e alinhamentos.
  - Modelos de dados que representam as entidades do jogo (ex: `FichaPersonagem`, `Raca`).
  - Responsável pela manipulação e persistência básica em memória ou arquivo.

- **DnDBot.Bot**  
  Contém a lógica de integração com o Discord:
  - Comandos slash, handlers de modais e interações.
  - Uso da biblioteca Discord.Net para criação e gestão das interações do bot.
  - Classes que respondem a eventos, coletam dados dos usuários e executam a lógica de aplicação.
  
- **Data**  
  Pasta onde ficam os arquivos JSON com dados estáticos:
  - Exemplo: `racas.json` que armazena as raças e sub-raças, para fácil manutenção e expansão sem precisar recompilar.

- **Outros projetos (Domain, Infrastructure, Shared)**  
  Preparados para possíveis expansões do sistema, como persistência em banco de dados, integração com APIs externas, etc.

---

## Serviços Detalhados

### RacasService

- Carrega o arquivo `Data/racas.json` para popular uma lista de raças com nome, descrição e sub-raças.
- Fornece métodos para obter a lista completa de raças para uso nos selects do Discord.
- Permite futura expansão para carregar ou modificar dinamicamente as raças.

### ClassesService

- Mantém uma lista estática das classes disponíveis no jogo (exemplo: Bárbaro, Mago, Guerreiro).
- Fornece método para obter a lista, usada no select do Discord.

### AntecedentesService

- Lista fixa dos antecedentes (backgrounds) usados no jogo.
- Método para obtenção e uso dinâmico no select.

### AlinhamentosService

- Lista os nove alinhamentos clássicos do D&D, facilitando a escolha durante a criação da ficha.

### FichaService

- Responsável por armazenar, recuperar e listar as fichas dos jogadores.
- Controla a persistência em memória (pode ser adaptado para banco de dados futuramente).
- Garante que cada jogador possa ter múltiplas fichas e que elas sejam recuperadas facilmente.

---

## Fluxo Completo da Criação da Ficha

1. **Comando `/ficha_criar`**  
   O bot abre um modal solicitando o nome do personagem.

2. **Modal preenchido pelo usuário**  
   O nome é validado; se inválido, o bot responde com erro.

3. **Montagem dos selects**  
   Depois do nome válido, o bot envia menus suspensos para escolha de Raça, Classe, Antecedente e Alinhamento, carregando os dados dinamicamente dos serviços.

4. **Escolha dos detalhes**  
   Cada escolha feita pelo usuário atualiza uma ficha temporária armazenada em memória.

5. **Validação final**  
   Quando todas as opções estiverem selecionadas e válidas, a ficha é salva permanentemente e o bot confirma a criação com um resumo dos dados.

6. **Comando `/ficha_ver`**  
   O usuário pode visualizar todas suas fichas criadas, apresentadas via embed no Discord, facilitando a consulta rápida.

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

### Exemplo JSON para Raças (Data/racas.json)

```json
[
  {
    "Nome": "Anão",
    "Descricao": "Robusto e resistente.",
    "SubRacas": ["Anão da Colina", "Anão da Montanha"]
  },
  {
    "Nome": "Elfo",
    "Descricao": "Ágil e de sentidos aguçados.",
    "SubRacas": ["Alto Elfo", "Elfo da Floresta"]
  }
]

## Como Rodar
Configure a variável de ambiente DISCORD_TOKEN com o token do seu bot Discord.

Compile e execute o projeto DnDBot.Bot.

No servidor Discord, utilize os comandos:

/roll para rolar dados.

/ficha_criar para criar uma ficha de personagem.

/ficha_ver para visualizar suas fichas.


## Exemplos de Uso

Usuário: /ficha_criar
Bot: Modal abre pedindo nome do personagem.
Usuário: Zephyr
Bot: Mostra selects para raça, classe, antecedente e alinhamento.
Usuário: Seleciona as opções.
Bot: ? Ficha do personagem criada com sucesso!
Nome: Zephyr  
Raça: Elfo  
Classe: Mago  
Antecedente: Sábio  
Alinhamento: Neutro e Bom  


## Como Contribuir
Abra issues para reportar bugs ou sugerir melhorias.

Envie pull requests para adicionar funcionalidades ou corrigir problemas.

Atualize o arquivo JSON Data/racas.json para modificar ou incluir novas raças.

Expanda os serviços para Classes, Antecedentes e Alinhamentos conforme necessidade.

Mantenha o padrão de modularidade e separação de responsabilidades.

Estrutura do Projeto
/DnDBot
 +- /Bot
 ¦    +- Commands
 ¦    ¦    +- ComandosFichaPersonagem.cs
 ¦    ¦    +- Outros comandos...
 ¦    +- ...
 +- /Application
 ¦    +- Services
 ¦    ¦    +- RacasService.cs
 ¦    ¦    +- ClassesService.cs
 ¦    ¦    +- AntecedentesService.cs
 ¦    ¦    +- AlinhamentosService.cs
 ¦    ¦    +- FichaService.cs
 ¦    +- Models
 ¦    ¦    +- FichaPersonagem.cs
 ¦    ¦    +- Raca.cs
 ¦    +- ...
 +- /Data
 ¦    +- racas.json
 +- Program.cs

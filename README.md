# DnDBot

Aplicação bot para automação e suporte a jogos de RPG de mesa, com foco em Dungeons & Dragons 5ª Edição, integrada ao Discord.

Desenvolvido com arquitetura modular e práticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gestão de rolagens de dados, criação e visualização de fichas de personagem, além de suporte para eventos e comandos customizados dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extensível para criação de ferramentas customizadas para campanhas digitais, otimizando a experiência dos jogadores e mestres por meio da automação de tarefas repetitivas e complexas.

---

## Funcionalidades Principais

- Comando slash /roll para rolar uma ou mais combinações de dados no formato NdX+Y (ex: 2d6+2, 1d10+5), com suporte a múltiplas expressões na mesma rolagem e modificadores.
- Comandos /roll_vantagem e /roll_desvantagem, que rolam dois dados e escolhem o maior ou menor valor, respectivamente, para casos de vantagem ou desvantagem.
- Sistema completo para criação, edição, distribuição de atributos e visualização de fichas de personagem.
- Distribuição de atributos interativa com sistema point-buy e validações automáticas.
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

## Fluxo Completo da Criação da Ficha

1. **Comando `/ficha_criar`**  
   O bot abre um modal solicitando o nome do personagem.

2. **Modal preenchido pelo usuário**  
   O nome é validado; se inválido, o bot responde com erro.

3. **Montagem dos selects**  
   Depois do nome válido, o bot envia menus suspensos para escolha de Raça, Classe, Antecedente e Alinhamento, carregando os dados dinamicamente dos serviços.

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
    public ulong JogadorId { get; set; }
    public string Nome { get; set; }
    public string Raca { get; set; }
    public string Classe { get; set; }
    public string Antecedente { get; set; }
    public string Alinhamento { get; set; }
    public int Forca { get; set; }
    public int Destreza { get; set; }
    public int Constituicao { get; set; }
    public int Inteligencia { get; set; }
    public int Sabedoria { get; set; }
    public int Carisma { get; set; }
}
```

### Raca.cs

```csharp
public class Raca
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public List<SubRaca> SubRacas { get; set; }
}
```

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
```

---

## Como Rodar

1. Configure a variável de ambiente `DISCORD_TOKEN` com o token do seu bot Discord.
2. Compile e execute o projeto `DnDBot.Bot`.
3. No servidor Discord, utilize os comandos:

- `/roll` para rolar dados.
- `/ficha_criar` para criar uma ficha de personagem.
- `/ficha_atributos` para distribuir atributos.
- `/ficha_ver` para visualizar suas fichas.

---

## Exemplos de Uso

```bash
Usuário: /ficha_criar
Bot: Modal abre pedindo nome do personagem.
Usuário: Zephyr
Bot: Mostra selects para raça, classe, antecedente e alinhamento.
Usuário: Seleciona as opções.
Bot: Envia interface interativa de distribuição de atributos.
Usuário: Distribui os pontos com os botões + e -.
Bot: ? Ficha do personagem criada com sucesso!
```

---

## Como Contribuir

- Abra issues para reportar bugs ou sugerir melhorias.
- Envie pull requests para adicionar funcionalidades ou corrigir problemas.
- Atualize os arquivos JSON em `/Data` para modificar ou incluir novas raças e sub-raças.
- Expanda os serviços para suportar novas mecânicas do D&D.
- Mantenha o padrão de modularidade e separação de responsabilidades.

---

## Estrutura do Projeto

```
/DnDBot
 +- /Bot
 ¦    +- Commands
 ¦    ¦    +- ComandoCriarFicha.cs
 ¦    ¦    +- ComandoAtributosFicha.cs
 ¦    ¦    +- Outros comandos...
 ¦    +- ...
 +- /Application
 ¦    +- Services
 ¦    ¦    +- RacasService.cs
 ¦    ¦    +- ClassesService.cs
 ¦    ¦    +- AntecedentesService.cs
 ¦    ¦    +- AlinhamentosService.cs
 ¦    ¦    +- FichaService.cs
 ¦    ¦    +- DistribuicaoAtributosService.cs
 ¦    ¦    +- DistribuicaoAtributosHandler.cs
 ¦    +- Models
 ¦    ¦    +- FichaPersonagem.cs
 ¦    ¦    +- Raca.cs
 ¦    ¦    +- SubRaca.cs
 ¦    ¦    +- DistribuicaoAtributosTemp.cs
 +- /Data
 ¦    +- racas.json
 +- Program.cs
```
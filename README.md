# DnDBot

Aplica��o bot para automa��o e suporte a jogos de RPG de mesa, com foco em Dungeons & Dragons 5� Edi��o, integrada ao Discord.

Desenvolvido com arquitetura modular e pr�ticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gest�o de rolagens de dados, cria��o e visualiza��o de fichas de personagem, al�m de suporte para eventos e comandos customizados dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extens�vel para cria��o de ferramentas customizadas para campanhas digitais, otimizando a experi�ncia dos jogadores e mestres por meio da automa��o de tarefas repetitivas e complexas.

---

## Funcionalidades Principais

- Comando slash /roll para rolar uma ou mais combina��es de dados no formato NdX+Y (ex: 2d6+2, 1d10+5), com suporte a m�ltiplas express�es na mesma rolagem e modificadores.
- Comandos /roll_vantagem e /roll_desvantagem, que rolam dois dados e escolhem o maior ou menor valor, respectivamente, para casos de vantagem ou desvantagem.
- Sistema completo para cria��o, edi��o, distribui��o de atributos e visualiza��o de fichas de personagem.
- Distribui��o de atributos interativa com sistema point-buy e valida��es autom�ticas.
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

## Fluxo Completo da Cria��o da Ficha

1. **Comando `/ficha_criar`**  
   O bot abre um modal solicitando o nome do personagem.

2. **Modal preenchido pelo usu�rio**  
   O nome � validado; se inv�lido, o bot responde com erro.

3. **Montagem dos selects**  
   Depois do nome v�lido, o bot envia menus suspensos para escolha de Ra�a, Classe, Antecedente e Alinhamento, carregando os dados dinamicamente dos servi�os.

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

1. Configure a vari�vel de ambiente `DISCORD_TOKEN` com o token do seu bot Discord.
2. Compile e execute o projeto `DnDBot.Bot`.
3. No servidor Discord, utilize os comandos:

- `/roll` para rolar dados.
- `/ficha_criar` para criar uma ficha de personagem.
- `/ficha_atributos` para distribuir atributos.
- `/ficha_ver` para visualizar suas fichas.

---

## Exemplos de Uso

```bash
Usu�rio: /ficha_criar
Bot: Modal abre pedindo nome do personagem.
Usu�rio: Zephyr
Bot: Mostra selects para ra�a, classe, antecedente e alinhamento.
Usu�rio: Seleciona as op��es.
Bot: Envia interface interativa de distribui��o de atributos.
Usu�rio: Distribui os pontos com os bot�es + e -.
Bot: ? Ficha do personagem criada com sucesso!
```

---

## Como Contribuir

- Abra issues para reportar bugs ou sugerir melhorias.
- Envie pull requests para adicionar funcionalidades ou corrigir problemas.
- Atualize os arquivos JSON em `/Data` para modificar ou incluir novas ra�as e sub-ra�as.
- Expanda os servi�os para suportar novas mec�nicas do D&D.
- Mantenha o padr�o de modularidade e separa��o de responsabilidades.

---

## Estrutura do Projeto

```
/DnDBot
 +- /Bot
 �    +- Commands
 �    �    +- ComandoCriarFicha.cs
 �    �    +- ComandoAtributosFicha.cs
 �    �    +- Outros comandos...
 �    +- ...
 +- /Application
 �    +- Services
 �    �    +- RacasService.cs
 �    �    +- ClassesService.cs
 �    �    +- AntecedentesService.cs
 �    �    +- AlinhamentosService.cs
 �    �    +- FichaService.cs
 �    �    +- DistribuicaoAtributosService.cs
 �    �    +- DistribuicaoAtributosHandler.cs
 �    +- Models
 �    �    +- FichaPersonagem.cs
 �    �    +- Raca.cs
 �    �    +- SubRaca.cs
 �    �    +- DistribuicaoAtributosTemp.cs
 +- /Data
 �    +- racas.json
 +- Program.cs
```
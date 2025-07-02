# Changelog

Todas as altera��es significativas neste projeto ser�o documentadas neste arquivo.

O formato segue o padr�o [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

---

## [1.1.3] - 2025-07-02

### Adicionado
- Servi�os para gerenciamento detalhado de per�cias e antecedentes, incluindo:
  - Classe `Pericia` com propriedades para tipo, atributo base, profici�ncia, b�nus e descri��o.
  - `AntecedentesService` para carregar e consultar antecedentes a partir de JSON, com cache interno e m�todos para buscar por ID e nome.
- Implementa��o do sistema de moedas e tesouro, com m�todos para manipular moedas (PC, PP, PO, PL, DA) e registrar hist�rico financeiro do personagem.
- M�todos `ObterRacaPorId` e `ObterSubRacaPorId` adicionados ao `RacasService` com valida��es para ignorar IDs n�o definidos.
- M�todo `ObterAlinhamentoPorId` adicionado ao `AlinhamentosService`.
- Ajustes no comando `/ficha_ver` para tratar IDs n�o definidos, exibindo o ID diretamente quando o nome n�o estiver dispon�vel, prevenindo erros e melhorando a robustez da exibi��o da ficha.
- Corre��o de timeout em intera��es Discord, garantindo que todas as respostas sejam enviadas dentro do limite de 3 segundos.
- Uso consistente de buscas case-insensitive para IDs e nomes em todos os servi�os.

### Corrigido
- Erro ao tentar obter nomes para IDs `"N�oDefinido"` ou vazios que causavam exce��es durante a visualiza��o da ficha.
- Timeout de intera��o no Discord em handlers de sele��o (sub-ra�a, classe) corrigido.
- Melhoria no tratamento de exce��es em intera��es, garantindo feedback ao usu�rio e evitando falhas silenciosas.

---

## [1.1.2] - 2025-07-01

### Adicionado
- Sistema completo de distribui��o de atributos baseado no m�todo Point Buy (27 pontos) integrado ao Discord.
- Comando `/ficha_atributos` com menu suspenso para sele��o de ficha.
- Exibi��o interativa dos atributos via embed, com bot�es para aumentar ou reduzir valores.
- Persist�ncia dos pontos de atributos na ficha ao concluir a distribui��o.
- Valida��es para impedir valores inv�lidos (abaixo de 8 ou acima de 15) ou ultrapassar o total de pontos dispon�veis.
- Classe `DistribuicaoAtributosHandler` para gerenciar a l�gica de distribui��o e renderiza��o dos bot�es e embed.
- Suporte a bot�es din�micos (`atributo_mais_x`, `atributo_menos_x`) com uso de `ComponentInteraction("atributo_*_*")`.
- Atualiza��o autom�tica da ficha com os atributos definidos ap�s clicar em "? Concluir".

### Corrigido
- Problemas de consist�ncia nos nomes dos atributos (remo��o de acentos e padroniza��o de chaves como "forca", "destreza" etc.).
- Corre��o de erros de convers�o ao configurar bot�es (`WithButton`), uso correto de `customId` e `row`.

### Removido
- Mapas auxiliares desnecess�rios como `AtributosMap` e `NomeAcentuado`, substitu�dos por chaves diretas e padronizadas.

---

## [1.1.1] - 2025-06-30

### Adicionado
- Redefini��o do modelo de dados para ra�as e sub-ra�as:
- A classe Raca agora cont�m uma lista de objetos SubRaca, ao inv�s de apenas strings com os IDs.
- A classe SubRaca foi expandida para incluir todos os atributos relevantes: b�nus de atributos, tend�ncias, tamanho, deslocamento, idiomas, profici�ncias, vis�o no escuro, resist�ncias, caracter�sticas, magias raciais e URLs de �cone/imagem.
- Estrutura JSON de ra�as atualizada para conter os dados completos das sub-ra�as dentro do campo subRacas.

---

## [1.1.0] - 2025-06-30

### Adicionado
- Implementa��o completa do sistema de cria��o de fichas de personagem via modais e menus suspensos no Discord.
- Servi�os dedicados para gerenciamento de ra�as, classes, antecedentes e alinhamentos.
- Armazenamento tempor�rio de fichas incompletas para cria��o passo a passo.
- Comando `/ficha_ver` para visualiza��o das fichas criadas por jogador.
- Helpers para gera��o din�mica dos selects com op��es de escolha.
- Documenta��o detalhada e coment�rios no c�digo para facilitar contribui��es.
- Arquitetura modular clara e extens�vel, dividida em camadas (`Application`, `Bot`, `Data`).

### Corrigido
- Valida��o refor�ada para impedir cria��o de fichas com dados inv�lidos ou incompletos.
- Tratamento de erros e mensagens de resposta para intera��es incompletas.

### Removido
- C�digo redundante e comandos n�o utilizados para simplifica��o do projeto.

---

## [1.0.0] - 2025-06-28

### Adicionado
- Estrutura modular dividida em camadas (Application, Bot, Domain, Infrastructure, Shared).
- Servi�o de rolagem de dados com suporte a express�es do tipo `NdX+Y`.
- Comando Slash `/roll` com resposta interativa.
- Configura��o via vari�veis de ambiente (`DISCORD_TOKEN`).
- Conex�o com Discord usando `Discord.Net v3.17.4`.
- Projeto hospedado no GitHub.
- Coment�rios explicativos no c�digo para fins educacionais.

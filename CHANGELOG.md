# Changelog

Todas as altera��es significativas neste projeto ser�o documentadas neste arquivo.

O formato segue o padr�o [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

## [1.3.1] - 2025-07-06

### Melhorado
- Refatora��o do handler `SelectSubracaHandler` para garantir que os idiomas da ficha sejam carregados antes da atualiza��o, prevenindo perda de idiomas existentes.
- Ajuste na l�gica de adi��o de idiomas para evitar duplicatas e preservar idiomas vindos de antecedentes ou outras fontes.
- Implementa��o de filtro para atribuir categoria padr�o `CategoriaIdioma.Standard` aos idiomas da sub-ra�a que n�o possu�am categoria definida.
- Melhoria na resposta do comando `btn_idiomas_*` com mensagem formatada, utilizando Embed do Discord para exibir idiomas conhecidos de forma clara e organizada.
- Valida��o mais robusta para garantir que a ficha pertence ao usu�rio antes de mostrar informa��es sens�veis.
- Logs detalhados adicionados para auxiliar no diagn�stico durante a sele��o e aplica��o de sub-ra�a.

### Corrigido
- Corre��o do problema onde os idiomas salvos n�o apareciam na ficha ap�s atualiza��o da sub-ra�a.
- Corre��o de exibi��o incorreta ou vazia da lista de idiomas no comando de visualiza��o.
- Evitado erro de duplica��o de idiomas ao aplicar sub-ra�a, respeitando os idiomas j� existentes na ficha.
- Preven��o de erros ao tentar acessar ou manipular idiomas quando a lista estava nula.


## [1.3.0] - 2025-07-06

### Adicionado
- Implementa��o da classe `Inventario` para gerenciamento completo dos itens do personagem, com controle de peso, adi��o, remo��o e hist�rico de a��es.
- Cria��o da classe `Equipamento` para gerenciamento dos itens equipados, com m�todos para equipar, desequipar e obter itens por slot.
- Integra��o entre `FichaPersonagem` e `Inventario`, garantindo associa��o direta e facilitando manipula��o de itens e moedas.
- Sistema detalhado de moedas (`BolsaDeMoedas` e `Moeda`), com m�todos para adicionar, remover e converter entre diferentes tipos de moedas.
- M�todos auxiliares na ficha para obter valores totais de atributos com b�nus e calcular modificadores automaticamente.
- Hist�rico detalhado das opera��es no invent�rio, permitindo rastrear adi��es e remo��es de itens.
- M�todos para listar itens por categoria, verificar quantidade dispon�vel e limpar o invent�rio.
- Propriedade para acesso f�cil aos equipamentos atualmente equipados pelo personagem.

### Melhorado
- Padroniza��o da estrutura de dados para itens e moedas, garantindo compatibilidade com Entity Framework Core e SQLite.
- Melhor controle sobre persist�ncia dos dados de invent�rio e equipamentos, reduzindo erros no banco e facilitando futuras migra��es.
- Documenta��o e coment�rios detalhados para facilitar manuten��o e uso do sistema de invent�rio dentro do projeto.


## [1.2.3] - 2025-07-04

### Adicionado
- Helpers est�ticos para cria��o e povoamento das tabelas SQLite de `Raca`, `Alinhamento` e `Pericia`.
- M�todos para cria��o das tabelas com defini��o completa de colunas, chaves prim�rias e estrangeiras.
- Fun��es para popular dados nas tabelas a partir de arquivos JSON (`racas.json`, `pericias.json`), incluindo tratamento de listas e enums.
- Uso consistente de comandos SQLite parametrizados para inser��es seguras.
- Tratamento especial para campos herdados da `EntidadeBase` nas inser��es.
- Inclus�o de m�todos auxiliares para inser��o de dados relacionados, como sub-ra�as, tags e dificuldades de per�cias.
- Coment�rios e documenta��o para facilitar manuten��o e futuras implementa��es.


## [1.2.2] - 2025-07-03

### Adicionado
- Cria��o da classe abstrata `EntidadeBase`, contendo propriedades comuns entre entidades como `Id`, `Nome`, `Descricao`, `Fonte`, `Pagina`, `Versao`, `ImagemUrl`, `IconeUrl`, `CriadoPor`, `CriadoEm`, `ModificadoPor`, `ModificadoEm` e `Tags`, al�m de m�todos utilit�rios para manipula��o de tags.
- Documenta��o XML completa da `EntidadeBase`, padronizando explica��es e inten��es de uso de cada campo e m�todo.

### Atualizado
- Refatora��o das classes que compartilham atributos semelhantes (`Classe`, `Antecedente`, `Raca`, etc.) para herdar de `EntidadeBase`, reduzindo duplica��o de c�digo e melhorando a manuten��o.
- Ajuste nos servi�os e comandos impactados para funcionar com a nova estrutura base comum.

---

## [1.2.1] - 2025-07-03

### Adicionado
- Novos modelos detalhados para armas e armaduras com suporte a propriedades como durabilidade, b�nus m�gicos, categorias, tipos de dano, propriedades especiais, raridade e fabricante.
- M�todos auxiliares para manipula��o de durabilidade (aplicar dano, reparar), c�lculo de dano total e m�ximo para armas, verifica��o de requisitos de atributos para uso de armas, controle de muni��o e recarga.
- Implementa��o da classe `Pericia` com suporte a m�ltiplos atributos base e alternativos, tipos variados, n�veis de dificuldade, especializa��o e v�nculo com classes relacionadas.
- Expans�o do modelo `SubRaca` para incluir caracter�sticas complexas como b�nus de atributo, resist�ncias, magias raciais, idiomas, vis�o no escuro, tamanhos e deslocamento.
- Cria��o da classe `Equipamento` simples para representa��o de itens com quantidade.
- Implementa��o do armazenamento tempor�rio `FichaTempStore` para manipula��o e atualiza��o parcial das fichas de personagem durante a cria��o.
- M�todos utilit�rios nas classes para facilitar opera��es comuns, como verifica��o de propriedades, manipula��o de magias associadas e c�lculo de atributos derivados.
- Inclus�o de documenta��o detalhada (XML) para todas as novas classes e m�todos.

### Atualizado
- Amplia��o dos servi�os para suportar os novos atributos e estruturas de dados, preparando para integra��o com UI/UX e persist�ncia futura.
- Padroniza��o da nomenclatura e documenta��o do c�digo para melhor manuten��o e entendimento.

### Corrigido
- Ajustes nas propriedades e m�todos para prevenir inconsist�ncias em valores nulos e listas vazias.
- Corre��es de l�gica em m�todos de verifica��o de requisitos para uso correto dos atributos do personagem.

---

## [1.2.0] - 2025-07-02

### Adicionado
- Integra��o com banco de dados SQLite para persist�ncia local dos dados.
- Cria��o automatizada da base `dndbot.db` e execu��o dos comandos SQL de cria��o de tabelas.
- Tabelas de entidades principais:
  - `FichaPersonagem`
  - `Tesouro`
- Tabelas auxiliares para listas da ficha:
  - `Ficha_Proficiencia`
  - `Ficha_Idioma`
  - `Ficha_Resistencia`
  - `Ficha_Caracteristica`
  - `Ficha_MagiaRacial`
  - `HistoricoFinanceiro`
- Tabelas de cat�logo adicionadas:
  - `Raca`, `SubRaca`, `Classe`, `Antecedente`, `Alinhamento`, `Proficiencia`, `Idioma`, `Resistencia`, `Caracteristica`, `Magia`
- Novo servi�o para conex�o SQLite com `SQLiteConnection` configurado com `Data Source`.

### Atualizado
- Documenta��o expandida no `README.md`, incluindo se��o de banco de dados e estrutura das tabelas.
- Atualiza��o do modelo `FichaPersonagem` para refletir novas propriedades persistidas.

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

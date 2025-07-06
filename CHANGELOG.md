# Changelog

Todas as alterações significativas neste projeto serão documentadas neste arquivo.

O formato segue o padrão [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

## [1.3.1] - 2025-07-06

### Melhorado
- Refatoração do handler `SelectSubracaHandler` para garantir que os idiomas da ficha sejam carregados antes da atualização, prevenindo perda de idiomas existentes.
- Ajuste na lógica de adição de idiomas para evitar duplicatas e preservar idiomas vindos de antecedentes ou outras fontes.
- Implementação de filtro para atribuir categoria padrão `CategoriaIdioma.Standard` aos idiomas da sub-raça que não possuíam categoria definida.
- Melhoria na resposta do comando `btn_idiomas_*` com mensagem formatada, utilizando Embed do Discord para exibir idiomas conhecidos de forma clara e organizada.
- Validação mais robusta para garantir que a ficha pertence ao usuário antes de mostrar informações sensíveis.
- Logs detalhados adicionados para auxiliar no diagnóstico durante a seleção e aplicação de sub-raça.

### Corrigido
- Correção do problema onde os idiomas salvos não apareciam na ficha após atualização da sub-raça.
- Correção de exibição incorreta ou vazia da lista de idiomas no comando de visualização.
- Evitado erro de duplicação de idiomas ao aplicar sub-raça, respeitando os idiomas já existentes na ficha.
- Prevenção de erros ao tentar acessar ou manipular idiomas quando a lista estava nula.


## [1.3.0] - 2025-07-06

### Adicionado
- Implementação da classe `Inventario` para gerenciamento completo dos itens do personagem, com controle de peso, adição, remoção e histórico de ações.
- Criação da classe `Equipamento` para gerenciamento dos itens equipados, com métodos para equipar, desequipar e obter itens por slot.
- Integração entre `FichaPersonagem` e `Inventario`, garantindo associação direta e facilitando manipulação de itens e moedas.
- Sistema detalhado de moedas (`BolsaDeMoedas` e `Moeda`), com métodos para adicionar, remover e converter entre diferentes tipos de moedas.
- Métodos auxiliares na ficha para obter valores totais de atributos com bônus e calcular modificadores automaticamente.
- Histórico detalhado das operações no inventário, permitindo rastrear adições e remoções de itens.
- Métodos para listar itens por categoria, verificar quantidade disponível e limpar o inventário.
- Propriedade para acesso fácil aos equipamentos atualmente equipados pelo personagem.

### Melhorado
- Padronização da estrutura de dados para itens e moedas, garantindo compatibilidade com Entity Framework Core e SQLite.
- Melhor controle sobre persistência dos dados de inventário e equipamentos, reduzindo erros no banco e facilitando futuras migrações.
- Documentação e comentários detalhados para facilitar manutenção e uso do sistema de inventário dentro do projeto.


## [1.2.3] - 2025-07-04

### Adicionado
- Helpers estáticos para criação e povoamento das tabelas SQLite de `Raca`, `Alinhamento` e `Pericia`.
- Métodos para criação das tabelas com definição completa de colunas, chaves primárias e estrangeiras.
- Funções para popular dados nas tabelas a partir de arquivos JSON (`racas.json`, `pericias.json`), incluindo tratamento de listas e enums.
- Uso consistente de comandos SQLite parametrizados para inserções seguras.
- Tratamento especial para campos herdados da `EntidadeBase` nas inserções.
- Inclusão de métodos auxiliares para inserção de dados relacionados, como sub-raças, tags e dificuldades de perícias.
- Comentários e documentação para facilitar manutenção e futuras implementações.


## [1.2.2] - 2025-07-03

### Adicionado
- Criação da classe abstrata `EntidadeBase`, contendo propriedades comuns entre entidades como `Id`, `Nome`, `Descricao`, `Fonte`, `Pagina`, `Versao`, `ImagemUrl`, `IconeUrl`, `CriadoPor`, `CriadoEm`, `ModificadoPor`, `ModificadoEm` e `Tags`, além de métodos utilitários para manipulação de tags.
- Documentação XML completa da `EntidadeBase`, padronizando explicações e intenções de uso de cada campo e método.

### Atualizado
- Refatoração das classes que compartilham atributos semelhantes (`Classe`, `Antecedente`, `Raca`, etc.) para herdar de `EntidadeBase`, reduzindo duplicação de código e melhorando a manutenção.
- Ajuste nos serviços e comandos impactados para funcionar com a nova estrutura base comum.

---

## [1.2.1] - 2025-07-03

### Adicionado
- Novos modelos detalhados para armas e armaduras com suporte a propriedades como durabilidade, bônus mágicos, categorias, tipos de dano, propriedades especiais, raridade e fabricante.
- Métodos auxiliares para manipulação de durabilidade (aplicar dano, reparar), cálculo de dano total e máximo para armas, verificação de requisitos de atributos para uso de armas, controle de munição e recarga.
- Implementação da classe `Pericia` com suporte a múltiplos atributos base e alternativos, tipos variados, níveis de dificuldade, especialização e vínculo com classes relacionadas.
- Expansão do modelo `SubRaca` para incluir características complexas como bônus de atributo, resistências, magias raciais, idiomas, visão no escuro, tamanhos e deslocamento.
- Criação da classe `Equipamento` simples para representação de itens com quantidade.
- Implementação do armazenamento temporário `FichaTempStore` para manipulação e atualização parcial das fichas de personagem durante a criação.
- Métodos utilitários nas classes para facilitar operações comuns, como verificação de propriedades, manipulação de magias associadas e cálculo de atributos derivados.
- Inclusão de documentação detalhada (XML) para todas as novas classes e métodos.

### Atualizado
- Ampliação dos serviços para suportar os novos atributos e estruturas de dados, preparando para integração com UI/UX e persistência futura.
- Padronização da nomenclatura e documentação do código para melhor manutenção e entendimento.

### Corrigido
- Ajustes nas propriedades e métodos para prevenir inconsistências em valores nulos e listas vazias.
- Correções de lógica em métodos de verificação de requisitos para uso correto dos atributos do personagem.

---

## [1.2.0] - 2025-07-02

### Adicionado
- Integração com banco de dados SQLite para persistência local dos dados.
- Criação automatizada da base `dndbot.db` e execução dos comandos SQL de criação de tabelas.
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
- Tabelas de catálogo adicionadas:
  - `Raca`, `SubRaca`, `Classe`, `Antecedente`, `Alinhamento`, `Proficiencia`, `Idioma`, `Resistencia`, `Caracteristica`, `Magia`
- Novo serviço para conexão SQLite com `SQLiteConnection` configurado com `Data Source`.

### Atualizado
- Documentação expandida no `README.md`, incluindo seção de banco de dados e estrutura das tabelas.
- Atualização do modelo `FichaPersonagem` para refletir novas propriedades persistidas.

---

## [1.1.3] - 2025-07-02

### Adicionado
- Serviços para gerenciamento detalhado de perícias e antecedentes, incluindo:
  - Classe `Pericia` com propriedades para tipo, atributo base, proficiência, bônus e descrição.
  - `AntecedentesService` para carregar e consultar antecedentes a partir de JSON, com cache interno e métodos para buscar por ID e nome.
- Implementação do sistema de moedas e tesouro, com métodos para manipular moedas (PC, PP, PO, PL, DA) e registrar histórico financeiro do personagem.
- Métodos `ObterRacaPorId` e `ObterSubRacaPorId` adicionados ao `RacasService` com validações para ignorar IDs não definidos.
- Método `ObterAlinhamentoPorId` adicionado ao `AlinhamentosService`.
- Ajustes no comando `/ficha_ver` para tratar IDs não definidos, exibindo o ID diretamente quando o nome não estiver disponível, prevenindo erros e melhorando a robustez da exibição da ficha.
- Correção de timeout em interações Discord, garantindo que todas as respostas sejam enviadas dentro do limite de 3 segundos.
- Uso consistente de buscas case-insensitive para IDs e nomes em todos os serviços.

### Corrigido
- Erro ao tentar obter nomes para IDs `"NãoDefinido"` ou vazios que causavam exceções durante a visualização da ficha.
- Timeout de interação no Discord em handlers de seleção (sub-raça, classe) corrigido.
- Melhoria no tratamento de exceções em interações, garantindo feedback ao usuário e evitando falhas silenciosas.

---

## [1.1.2] - 2025-07-01

### Adicionado
- Sistema completo de distribuição de atributos baseado no método Point Buy (27 pontos) integrado ao Discord.
- Comando `/ficha_atributos` com menu suspenso para seleção de ficha.
- Exibição interativa dos atributos via embed, com botões para aumentar ou reduzir valores.
- Persistência dos pontos de atributos na ficha ao concluir a distribuição.
- Validações para impedir valores inválidos (abaixo de 8 ou acima de 15) ou ultrapassar o total de pontos disponíveis.
- Classe `DistribuicaoAtributosHandler` para gerenciar a lógica de distribuição e renderização dos botões e embed.
- Suporte a botões dinâmicos (`atributo_mais_x`, `atributo_menos_x`) com uso de `ComponentInteraction("atributo_*_*")`.
- Atualização automática da ficha com os atributos definidos após clicar em "? Concluir".

### Corrigido
- Problemas de consistência nos nomes dos atributos (remoção de acentos e padronização de chaves como "forca", "destreza" etc.).
- Correção de erros de conversão ao configurar botões (`WithButton`), uso correto de `customId` e `row`.

### Removido
- Mapas auxiliares desnecessários como `AtributosMap` e `NomeAcentuado`, substituídos por chaves diretas e padronizadas.

---

## [1.1.1] - 2025-06-30

### Adicionado
- Redefinição do modelo de dados para raças e sub-raças:
- A classe Raca agora contém uma lista de objetos SubRaca, ao invés de apenas strings com os IDs.
- A classe SubRaca foi expandida para incluir todos os atributos relevantes: bônus de atributos, tendências, tamanho, deslocamento, idiomas, proficiências, visão no escuro, resistências, características, magias raciais e URLs de ícone/imagem.
- Estrutura JSON de raças atualizada para conter os dados completos das sub-raças dentro do campo subRacas.

---

## [1.1.0] - 2025-06-30

### Adicionado
- Implementação completa do sistema de criação de fichas de personagem via modais e menus suspensos no Discord.
- Serviços dedicados para gerenciamento de raças, classes, antecedentes e alinhamentos.
- Armazenamento temporário de fichas incompletas para criação passo a passo.
- Comando `/ficha_ver` para visualização das fichas criadas por jogador.
- Helpers para geração dinâmica dos selects com opções de escolha.
- Documentação detalhada e comentários no código para facilitar contribuições.
- Arquitetura modular clara e extensível, dividida em camadas (`Application`, `Bot`, `Data`).

### Corrigido
- Validação reforçada para impedir criação de fichas com dados inválidos ou incompletos.
- Tratamento de erros e mensagens de resposta para interações incompletas.

### Removido
- Código redundante e comandos não utilizados para simplificação do projeto.

---

## [1.0.0] - 2025-06-28

### Adicionado
- Estrutura modular dividida em camadas (Application, Bot, Domain, Infrastructure, Shared).
- Serviço de rolagem de dados com suporte a expressões do tipo `NdX+Y`.
- Comando Slash `/roll` com resposta interativa.
- Configuração via variáveis de ambiente (`DISCORD_TOKEN`).
- Conexão com Discord usando `Discord.Net v3.17.4`.
- Projeto hospedado no GitHub.
- Comentários explicativos no código para fins educacionais.

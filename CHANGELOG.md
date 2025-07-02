# Changelog

Todas as alterações significativas neste projeto serão documentadas neste arquivo.

O formato segue o padrão [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

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

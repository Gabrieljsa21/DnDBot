# Changelog

Todas as alterações significativas neste projeto serão documentadas neste arquivo.

O formato segue o padrão [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

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

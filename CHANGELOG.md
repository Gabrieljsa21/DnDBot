# Changelog

Todas as altera��es significativas neste projeto ser�o documentadas neste arquivo.

O formato segue o padr�o [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

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

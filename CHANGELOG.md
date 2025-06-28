# Changelog

Todas as alterações significativas neste projeto serão documentadas neste arquivo.

O formato segue o padrão [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), e o versionamento segue [Semantic Versioning](https://semver.org/lang/pt-BR/).

---

## [1.0.0] - 2025-06-28

### Adicionado
- Estrutura modular dividida em camadas (Application, Bot, Domain, Infrastructure, Shared)
- Serviço de rolagem de dados com suporte a expressões do tipo `NdX+Y`
- Comando Slash `/roll` com resposta interativa
- Configuração via variáveis de ambiente (`DISCORD_TOKEN`)
- Conexão com Discord usando `Discord.Net v3.17.4`
- Projeto hospedado no GitHub
- Comentários explicativos no código para fins educacionais

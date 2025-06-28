# DnDBot

Aplicação bot para automação e suporte a jogos de RPG de mesa, com foco em Dungeons & Dragons 5ª Edição, integrada ao Discord.

Desenvolvido com arquitetura modular e práticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gestão de rolagens de dados e eventos dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extensível para criação de ferramentas customizadas para campanhas digitais, otimizando a experiência dos jogadores e mestres por meio da automação de tarefas repetitivas e complexas.

## Funcionalidades

- Comando slash `/roll` para rolar dados no formato NdX+Y (exemplo: 2d6+3).

## Estrutura do projeto

- `DnDBot.Application`: Serviços de lógica do jogo (ex: rolagem de dados).
- `DnDBot.Bot`: Código do bot Discord e comandos.
- Outros projetos (Domain, Infrastructure, Shared) prontos para expansão futura.

## Como rodar

1. Configure a variável de ambiente `DISCORD_TOKEN` com o token do seu bot Discord.
2. Compile e execute o projeto `DnDBot.Bot`.
3. No seu servidor Discord, use o comando `/roll` para testar a rolagem de dados.

## Como contribuir

- Abra issues para bugs ou sugestões.
- Envie pull requests para melhorias.


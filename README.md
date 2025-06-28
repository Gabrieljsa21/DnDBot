# DnDBot

Aplica��o bot para automa��o e suporte a jogos de RPG de mesa, com foco em Dungeons & Dragons 5� Edi��o, integrada ao Discord.

Desenvolvido com arquitetura modular e pr�ticas modernas, o DnDBot oferece comandos interativos via Slash Commands, facilitando a gest�o de rolagens de dados e eventos dentro do ambiente colaborativo do Discord.

Este projeto serve como uma base extens�vel para cria��o de ferramentas customizadas para campanhas digitais, otimizando a experi�ncia dos jogadores e mestres por meio da automa��o de tarefas repetitivas e complexas.

## Funcionalidades

- Comando slash `/roll` para rolar dados no formato NdX+Y (exemplo: 2d6+3).

## Estrutura do projeto

- `DnDBot.Application`: Servi�os de l�gica do jogo (ex: rolagem de dados).
- `DnDBot.Bot`: C�digo do bot Discord e comandos.
- Outros projetos (Domain, Infrastructure, Shared) prontos para expans�o futura.

## Como rodar

1. Configure a vari�vel de ambiente `DISCORD_TOKEN` com o token do seu bot Discord.
2. Compile e execute o projeto `DnDBot.Bot`.
3. No seu servidor Discord, use o comando `/roll` para testar a rolagem de dados.

## Como contribuir

- Abra issues para bugs ou sugest�es.
- Envie pull requests para melhorias.


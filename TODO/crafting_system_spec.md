**Sistema de Crafting / Ferreiro para Bot de Discord de DnD 5e**  

## 1. Visão Geral  
Definir um módulo de fabricação de itens (crafting) que permita aos jogadores produzir armas, armaduras, consumíveis e itens raros, respeitando regras oficiais de D&D 5e adaptadas ao ambiente de Discord.

## 2. Referências Oficiais  
- **Player’s Handbook (PHB)**: regras básicas de crafting e uso de ferramentas.  
- **Dungeon Master’s Guide (DMG)**: custos, tempo e variantes de crafting.  
- **Xanathar’s Guide to Everything (XGtE)**: opções de downtime e testes de habilidade.

## 3. Categorias de Itens Craftáveis  
| Categoria           | Exemplos                          | Disponibilidade                                 |
|---------------------|-----------------------------------|-------------------------------------------------|
| Itens Comuns        | Espadas, escudos, armaduras leves | Livre, rank mínimo: Ferro                       |
| Consumíveis Simples | Munições, kits de primeiros socorros | Livre, rank mínimo: Ferro                    |
| Itens Raros         | Poções, ferramentas especiais     | Reputação/facção mínima ou rank: Prata          |

> *Itens Mágicos e encantamentos serão tratados em documento separado.*

## 4. Requisitos para Iniciar  
- **Proficiência**: possuir o kit de ferreiro ou ferramenta apropriada.  
- **Localização**: estar em uma oficina (guilda ou cidade).  
- **Materiais**: ter componentes ou metade do valor de mercado em PO.  
- **Comando**: `/craft iniciar <item_id>` (ver seção de comandos).

## 5. Tempo de Produção Adaptado  
| Valor de Mercado (PO) | Tempo de Crafting no Bot      |
|-----------------------|-------------------------------|
| Até 50                | 1 hora real                   |
| 51–100                | 2 horas                       |
| 101–500               | 6 horas                       |
| 501–1000              | 12 horas                      |
| >1000                 | 24 horas                      |

> **Ajuda**: cada aliado proficiente reduz o tempo total em 1 bloco da tabela.

## 6. Sistema de Progresso  
- **Sessões de Trabalho**: cada `/craft trabalhar` consome 1 bloco de tempo e avança o progresso conforme cronograma.  
- **Acompanhamento**: `/craft progresso` mostra percentual concluído, tempo restante e participantes.

## 7. Falhas e Testes  
- **Itens Comuns/Consumíveis**: nenhuma verificação extra.  
- **Itens Raros**: exige um teste de ferramenta (atributo + proficiência, CD definido pelo DMG, ex: CD 15).  
  - **Sucesso**: progresso normal.  
  - **Falha**: avança metade do progresso ou consome materiais extras.

## 8. Ajuda de Aliados  
- Aliados com proficiência: cada um contribui com +1 sessão de trabalho por período.  
- **Comando**: líder convoca aliado com `/craft convidar @user`, que pode aceitar via reação.

## 9. Comandos Principais  
- `/craft iniciar <item_id>`: começa nova fila de crafting.  
- `/craft trabalhar`: consome um bloco de tempo e atualiza o progresso.  
- `/craft progresso`: exibe status atual do crafting.  
- `/craft cancelar <craft_id>`: aborta o processo e recupera metade dos materiais.

## 10. Integração com o Sistema de Guilda  
- **Rank Mínimo**: itens raros exigem rank mínimo (Prata ou superior).  
- **Reputação/Facções**: certos itens só disponíveis para jogadores com reputação positiva em facções específicas.  
- **Oficina da Guilda**: benefícios como redução de custo (-10% PO) ou tempo (-1 hora) para membros de ranks altos.

## 11. Especializações de Artesão  
Os jogadores podem escolher uma especialização baseada nas guildas de artesãos do PHB, conferindo bônus específicos (tempo reduzido, menor custo, testes mais favoráveis):

| d20 | Guilda (PT)                                             | Especialização                        |
|-----|----------------------------------------------------------|---------------------------------------|
| 1   | Alquimistas e boticários                                 | Cria poções e elixires                |
| 2   | Armadores, cadeieiros e ourives                          | Forja armaduras e fechaduras          |
| 3   | Cervejeiros, destiladores e vintners                     | Produção de bebidas                  |
| 4   | Calígrafos, escribas e copistas                          | Manuscritos, pergaminhos              |
| 5   | Carpinteiros, telhadores e estucadores                   | Itens de madeira e estruturas         |
| 6   | Cartógrafos, agrimensores e confeccionadores de mapas    | Mapas e instrumentos de navegação     |
| 7   | Sapateiros e fabricantes de calçados                      | Artefatos de couro                    |
| 8   | Cozinheiros e padeiros                                    | Comida e rações                       |
| 9   | Sopro de vidro e vidraceiros                             | Vidros e lentes                       |
| 10  | Joalheiros e lapidários                                   | Joias e gemas                         |
| 11  | Curtidores e tratadores de couro                          | Artefatos de couro refinados          |
| 12  | Pedreiros e cortadores de pedra                           | Itens de pedra e estruturas           |
| 13  | Pintores, iluminadores e letreiros                       | Pinturas e sinais                     |
| 14  | Oleiros e fabricantes de telhas                           | Cerâmica e azulejos                   |
| 15  | Carpinteiros navais e velameiros                         | Embarcações e velas                   |
| 16  | Ferreiros e metalúrgicos                                  | Armas e ferramentas de metal          |
| 17  | Funileiros, estanhadores e fundidores                     | Itens de metal leve e fundição        |
| 18  | Carroceiros e rodas                                        | Veículos e rodas                      |
| 19  | Tecelões e tintureiros                                    | Tecelagem e tingimento                |
| 20  | Entalhadores de madeira, tonéis e fabricantes de arcos    | Artefatos de madeira especializada    |

> Cada especialização concede:
> - **-10% no tempo de crafting** de itens da categoria;
> - **Teste de ferramenta** com vantagem ao criar itens específicos.

## 12. Mecânicas Extras  
1. **Qualidade dos Itens**: escala de qualidade (Comum, Superior, Excepcional, Mestre). No final do crafting, rolar 1d20 contra CD para determinar qualidade bônus.  
2. **Blueprints e Receitas Secretas**: algumas receitas bloqueadas até o jogador completar missões ou trocar com NPCs/facções; blueprints raros liberam técnicas avançadas.  
3. **Upgrades de Oficina**: recursos investidos para melhorar a oficina (e.g. bancada de forja, banco de encantamentos) que reduzem tempo ou aumentam chance de qualidade.  
4. **Crafting em Grupo (Projetos Maiores)**: projetos cooperativos que exigem múltiplos participantes e duram mais tempo, com recompensas e reputação elevadas.  
5. **Manutenção e Desgaste**: itens criados podem ter durabilidade e necessitar de reparos usando `/craft reparar <item_id>`, consumindo materiais.

## 13. Guildas como Facções Vivas
1. **Contratos Exclusivos por Guilda**: cada guilda oferece missões temáticas baseadas na especialização. Ex: Alquimistas mandam coletar reagentes raros; joalheiros pedem gemas exóticas. Desbloqueadas por reputação.
2. **Mercadores de Guilda (NPCs)**: vendem materiais e receitas secretas. Reputação influencia preço e acesso.
3. **Desafios de Habilidade**: eventos com tempo real e testes críticos de ferramenta. Ex: reparar algo em 60s ou perder materiais.
4. **Árvore de Progressão da Guilda**: reputação coletiva eleva o nível da guilda, desbloqueando perks globais.
5. **Alianças e Conflitos entre Guildas**: eventos de influência onde jogadores escolhem lados, afetando contratos, preços e relações.
6. **Campanhas Patrocinadas**: arcos de crafting longos em nome da guilda. Ex: forjar armas para um exército ou reconstruir uma cidade.
7. **Artefatos e Legados de Guilda**: itens únicos que só podem ser usados ou melhorados por membros de certa guilda e nível.

## 14. Próximos Passos  
- Definir lista completa de `item_id`, custo e requisitos.  
- Mapear CDs de testes de ferramenta para cada item raro.  
- Implementar lógica de convites e ajuda de aliados.  
- Criar testes de unidade para fluxo de crafting e mecânicas extras.

---
*Documento inicial para o módulo de crafting, pronto para detalhamento e implementação.*


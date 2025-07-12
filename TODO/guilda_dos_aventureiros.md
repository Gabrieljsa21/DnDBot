**Guilda dos Aventureiros – Sistema para Bot de Discord (D&D 5e)**

## 1. Visão Geral
A Guilda dos Aventureiros é a instituição central que organiza, valida e recompensa as ações dos personagens jogadores dentro do mundo. Ela atua como ponto de entrada para missões, progressão de ranking, reputação e interação com outras facções.

## 2. Funções Principais da Guilda

| Função                        | Descrição                                                                 |
|------------------------------|---------------------------------------------------------------------------|
| Mural de Missões             | Lista de missões solo ou em grupo, renovadas periodicamente              |
| Sistema de Ranks             | Progressão de aventureiro conforme reputação, missões e pontuação         |
| Histórico de Missões         | Registro de missões aceitas, em andamento e concluídas                   |
| Gerenciamento de Grupo       | Registro e administração de times fixos de jogadores                     |
| Reputação com Facções        | Varia conforme missões realizadas e decisões tomadas                     |
| Distribuição de Recompensas  | XP e ouro divididos igualmente; itens únicos sorteados entre o grupo     |
| Penalidades e Prazos         | Missões têm prazo, afetam score do jogador e reputação                   |

## 3. Sistema de Ranks

| Rank        | Progressão sugerida                      |
|-------------|-------------------------------------------|
| Ferro       | Inicial                                   |
| Bronze      | Após 3 missões                            |
| Aço         | Reputação ou conquistas extras            |
| Prata       | Acesso a missões maiores                  |
| Ouro        | Missões de elite                          |
| Platina     | Missões internacionais/interplanares      |
| Ametista    | Alto prestígio regional                   |
| Jade        | Autoridade local e influência             |
| Esmeralda   | Acesso a artefatos e contratos secretos   |
| Safira      | Agente de elite                           |
| Rubi        | Operações altamente sigilosas             |
| Diamante    | Operações mundiais                        |
| Mithral     | Lendas vivas                              |
| Adamantina  | Campeões consagrados                      |
| Oricalco    | Status mítico e autoridade máxima         |

## 4. Mural de Missões

### Funcionamento:
- `/mural` lista as missões disponíveis ao jogador.
- Se o jogador estiver em um grupo, é perguntado se deseja ver missões **solo** ou **em grupo**.
- Missões são **geradas a cada hora** por jogador (até 10 simultâneas).
- Sistema verifica a última geração e renova se necessário.

### Tipos de Missão:
- Coleta
- Subjugação
- Escolta
- Transporte
- Investigação
- Recuperação

### Variáveis de Missão:
- Duração (curta, média, longa)
- Dificuldade (fácil a mortal)
- Região ou facção contratante

## 5. Aceitação de Missão
- Só o **líder do grupo** pode aceitar uma missão em grupo.
- Aceitação feita por emoji na mensagem do mural.
- Missão aceita é registrada no histórico e contabiliza no progresso.

## 6. Conclusão de Missão
**Método escolhido:** Conclusão com Verificação de Progresso
- Jogador realiza testes durante a missão.
- Resultados influenciam sucesso parcial, total ou fracasso.
- Recompensas ajustadas conforme desempenho.
- Conclusão registra data, desempenho, e impacto na reputação e ranking.

## 7. Reputação com Facções
- Missões podem afetar a relação com facções diversas (ex: guardas locais, guildas, igreja, etc.).
- Reputação positiva desbloqueia acesso a recompensas, contratos e descontos.
- Reputação negativa pode bloquear missões, gerar emboscadas ou subir preço de serviços.

## 8. Histórico de Missões
- Cada jogador/grupo mantém um log de:
  - Missões ativas
  - Missões concluídas (com data e desempenho)
  - Missões fracassadas ou expiradas

## 9. Distribuição de Recompensas
- Ouro e XP divididos igualmente
- Itens únicos: sorteio entre os membros presentes
- Alguns contratos oferecem **bônus individuais** ou **recompensas por reputação**

## 10. Penalidades e Prazos
- Cada missão tem um prazo máximo para conclusão (ex: 48h reais)
- Se falhar no prazo:
  - Redução de reputação
  - Remoção da missão
  - Penalidade no ranking (dependendo da gravidade)

---
Documento inicial da Guilda dos Aventureiros, pronto para expansão com comandos, interações com o mundo e integrações com o mural e crafting.


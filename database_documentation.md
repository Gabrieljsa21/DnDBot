# Banco de Dados - DnDBot

Esta documentação descreve o esquema de banco de dados utilizado no projeto **DnDBot**, implementado em SQLite para armazenamento de fichas de personagens e dados relacionados.

---

## Estrutura Geral

### Tabela: `FichaPersonagem`
Contém as informações principais de cada personagem criado.

| Coluna           | Tipo     | Descrição                                   |
|------------------|----------|---------------------------------------------|
| Id               | TEXT     | Identificador único (GUID) - Primary Key    |
| IdJogador        | INTEGER  | ID do jogador no Discord                    |
| Nome             | TEXT     | Nome do personagem                          |
| IdRaca           | TEXT     | ID da raça                                 |
| IdSubraca        | TEXT     | ID da sub-raça                              |
| IdClasse         | TEXT     | ID da classe                                |
| IdAntecedente    | TEXT     | ID do antecedente                           |
| IdAlinhamento    | TEXT     | ID do alinhamento                           |
| Forca            | INTEGER  | Atributo base                               |
| Destreza         | INTEGER  | Atributo base                               |
| Constituicao     | INTEGER  | Atributo base                               |
| Inteligencia     | INTEGER  | Atributo base                               |
| Sabedoria        | INTEGER  | Atributo base                               |
| Carisma          | INTEGER  | Atributo base                               |
| Tamanho          | TEXT     | Tamanho do personagem                       |
| Deslocamento     | INTEGER  | Valor de deslocamento em metros            |
| VisaoNoEscuro    | INTEGER  | Alcance de visão no escuro (em metros)      |
| DataCriacao      | TEXT     | Data/hora da criação da ficha               |
| DataAlteracao    | TEXT     | Data/hora da última alteração             |
| EstaAtivo        | INTEGER  | Booleano: 1 para ativo, 0 para inativo      |

---

### Tabela: `Tesouro`
Armazena as moedas de um personagem.

| Coluna  | Tipo    | Descrição                      |
|---------|---------|----------------------------------|
| IdFicha | TEXT    | FK para FichaPersonagem          |
| PC      | INTEGER | Peças de Cobre                  |
| PP      | INTEGER | Peças de Prata                  |
| PO      | INTEGER | Peças de Ouro                   |
| PL      | INTEGER | Peças de Platina                |
| DA      | INTEGER | Diamantes ou moedas especiais    |

---

### Tabela: `HistoricoFinanceiro`
Registra eventos econômicos da ficha (gastos, ganhos, trocas).

| Coluna  | Tipo | Descrição                         |
|---------|------|-----------------------------------|
| IdFicha | TEXT | FK para FichaPersonagem           |
| Registro| TEXT | Descrição textual do evento       |

---

### Tabelas auxiliares (muitos para muitos)
Armazenam as relações entre ficha e listas de IDs.

- `Ficha_Proficiencia(IdFicha, Id)`
- `Ficha_Idioma(IdFicha, Id)`
- `Ficha_Resistencia(IdFicha, Id)`
- `Ficha_Caracteristica(IdFicha, Id)`
- `Ficha_MagiaRacial(IdFicha, Id)`

Todas essas tabelas possuem:

| Coluna  | Tipo | Descrição                  |
|---------|------|------------------------------|
| IdFicha | TEXT | FK para FichaPersonagem      |
| Id      | TEXT | ID da proficiência/idioma/etc |

---

## Diagrama ER

O diagrama visual `dndbot_database_schema.png` representa as relações entre as tabelas descritas acima.

---

## Observações

- Todos os campos booleanos são salvos como `INTEGER` (0 ou 1).
- Datas são armazenadas como `TEXT` no formato ISO (ex: `2025-07-02T14:00:00`).
- As listas auxiliares permitem expansão sem normalizar todas as entidades (por exemplo, "Idioma" pode vir de arquivos JSON).

---

## Futuras Expansões

- Tabela para armazenar Magias preparadas/livro de magias.
- Tabela para equipamentos e inventário.
- Relacionamentos com campanhas ou mestres.

---

*Documentação gerada automaticamente em 2025-07-02.*


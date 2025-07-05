\# Estrutura do Projeto DnDBot (Resumo por Camadas)



---



\## 📁 DnDBot.Application

Camada principal com os modelos, dados e lógica de aplicação.



---



\## 📁 Data

\- \*\*Configurations/\*\* → Arquivos `IEntityTypeConfiguration` (ex: `PericiaConfiguration.cs`, `ClasseConfiguration.cs`...)

\- \*\*DnDBotDbContext.cs\*\* → DbContext principal

\- \*\*DnDBotDbContextFactory.cs\*\* → Factory para scaffolding/migrations

\- Arquivos JSON de dados (`antecedentes.json`, `classes.json`, etc.)



---



\## 📁 Helpers

\- `PathHelper.cs`



---



\## 📁 Migrations

\- Gerado automaticamente pelo Entity Framework Core



---



\## 📁 Models

\- \*\*Antecedente/\*\* → `Antecedente.cs`, `Defeito.cs`, `Ideal.cs`, `Vinculo.cs`

\- \*\*Enums/\*\* → Enumerações do sistema

\- \*\*Ficha/\*\* → Modelos da ficha do personagem (`Equipamento.cs`, `Armadura.cs`, `Pericia.cs`, etc.)

\- \*\*Inputs/\*\* → Componentes de input do Discord (`ModalFichaNomeInput.cs`)



---



\## 📁 DnDBot.Bot

Responsável pela interação com o Discord (bot em si).



---



\## 📁 Commands

\- \*\*Ficha/\*\* → Comandos relacionados à criação e manipulação de ficha



---



\## 📁 Helpers

\- `SelectMenuHelper.cs`



---



\## 📁 Models, Services, Utils

Suporte à interface do bot



---



\## 📁 DnDBot.Domain

(Sem conteúdo mostrado — possivelmente reservado para entidades de domínio puro)



---



\## 📁 DnDBot.Infrastructure

(Sem conteúdo mostrado — usado normalmente para implementações externas, banco, etc.)



---



\## 📁 DnDBot.Shared

(Sem conteúdo mostrado — pode ser usado para utilitários ou modelos compartilhados)



---




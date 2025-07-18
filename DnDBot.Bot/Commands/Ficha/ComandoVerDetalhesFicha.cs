﻿using Discord;
using Discord.Interactions;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Antecedentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{

    /// <summary>
    /// Módulo responsável pelo comando /ficha_ver_todas que exibe fichas salvas do usuário.
    /// </summary>
    public class ComandoVerDetalhesFichas : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly RacasService _racasService;
        private readonly ClassesService _classesService;
        private readonly AntecedentesService _antecedentesService;
        private readonly AlinhamentosService _alinhamentosService;
        private readonly InventarioService _inventarioService;
        private readonly IdiomaService _idiomaService;
        private readonly ResistenciaService _resistenciaService;

        /// <summary>
        /// Construtor com injeção do serviço de fichas.
        /// </summary>
        public ComandoVerDetalhesFichas(
            FichaService fichaService,
            RacasService racasService,
            ClassesService classesService,
            AntecedentesService antecedentesService,
            AlinhamentosService alinhamentosService,
            InventarioService inventarioService,
            IdiomaService idiomaService,
            ResistenciaService resistenciaService
        )
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _classesService = classesService;
            _antecedentesService = antecedentesService;
            _alinhamentosService = alinhamentosService;
            _inventarioService = inventarioService;
            _idiomaService = idiomaService;
            _resistenciaService = resistenciaService;
        }

        [SlashCommand("ficha_detalhes", "Visualiza uma ficha específica")]
        public async Task VerFichaInterativaAsync()
        {
            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);

            if (fichas == null || fichas.Count == 0)
            {
                await RespondAsync("❌ Você não tem nenhuma ficha criada.", ephemeral: true);
                return;
            }

            if (fichas.Count == 1)
            {
                await DeferAsync(ephemeral: true);
                await MostrarFichaEmbedAsync(fichas[0]);
                return;
            }


            var menu = new SelectMenuBuilder()
                .WithCustomId("ficha_ver_uma_dropdown")
                .WithPlaceholder("Escolha a ficha que deseja ver");

            foreach (var ficha in fichas.Take(25))
            {
                menu.AddOption(ficha.Nome, ficha.Id.ToString());
            }

            var builder = new ComponentBuilder().WithSelectMenu(menu);
            await RespondAsync("📝 Selecione uma ficha para visualizar:", components: builder.Build(), ephemeral: true);
        }

        [ComponentInteraction("ficha_ver_uma_dropdown")]
        public async Task FichaSelecionadaDropdownHandler(string fichaIdStr)
        {
            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await RespondAsync("ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            await DeferAsync(ephemeral: true); 
            await MostrarFichaEmbedAsync(ficha);
        }

        private async Task MostrarFichaEmbedAsync(FichaPersonagem ficha)
        {
            var atributosTexto = new List<string>
    {
        $"Força: {_fichaService.FormatarAtributo(ficha, "Forca")}",
        $"Destreza: {_fichaService.FormatarAtributo(ficha, "Destreza")}",
        $"Constituição: {_fichaService.FormatarAtributo(ficha, "Constituicao")}",
        $"Inteligência: {_fichaService.FormatarAtributo(ficha, "Inteligencia")}",
        $"Sabedoria: {_fichaService.FormatarAtributo(ficha, "Sabedoria")}",
        $"Carisma: {_fichaService.FormatarAtributo(ficha, "Carisma")}"
    };

            string raca = string.IsNullOrWhiteSpace(ficha.RacaId) ? "Não definida"
                : (await _racasService.ObterRacaPorIdAsync(ficha.RacaId))?.Nome ?? ficha.RacaId;

            string subRaca = string.IsNullOrWhiteSpace(ficha.SubracaId) ? "Não definida"
                : (await _racasService.ObterSubRacaPorIdAsync(ficha.SubracaId))?.Nome ?? ficha.SubracaId;

            string classe = string.IsNullOrWhiteSpace(ficha.ClasseId) ? "Não definida"
                : (await _classesService.ObterClassePorIdAsync(ficha.ClasseId))?.Nome ?? ficha.ClasseId;

            string antecedente = string.IsNullOrWhiteSpace(ficha.AntecedenteId) ? "Não definido"
                : (await _antecedentesService.ObterAntecedentePorIdAsync(ficha.AntecedenteId))?.Nome ?? ficha.AntecedenteId;

            string alinhamento = string.IsNullOrWhiteSpace(ficha.AlinhamentoId) ? "Não definido"
                : (await _alinhamentosService.ObterAlinhamentoPorIdAsync(ficha.AlinhamentoId))?.Nome ?? ficha.AlinhamentoId;

            var embedBuilder = new EmbedBuilder()
                .WithTitle($"📘 Ficha: {ficha.Nome}")
                .WithColor(Color.DarkPurple)
                .AddField("Raça", raca, true)
                .AddField("Sub-Raça", subRaca, true)
                .AddField("Classe", classe, true)
                .AddField("Antecedente", antecedente, true)
                .AddField("Alinhamento", alinhamento, true)
                .AddField("🧠 Atributos", string.Join("\n", atributosTexto), false);

            if (!string.IsNullOrWhiteSpace(ficha.ImagemUrl))
            {
                embedBuilder.WithThumbnailUrl(ficha.ImagemUrl);
            }

            var embed = embedBuilder.Build();

            var components = new ComponentBuilder()
                .WithButton("Profic.", $"btn_proficiencias_{ficha.Id}", ButtonStyle.Primary, new Emoji("📜"))
                .WithButton("Idiomas", $"btn_idiomas_{ficha.Id}", ButtonStyle.Primary, new Emoji("🗣️"))
                .WithButton("Resist.", $"btn_resistencias_{ficha.Id}", ButtonStyle.Primary, new Emoji("🛡️"))
                .WithButton("Caract.", $"btn_caracteristicas_{ficha.Id}", ButtonStyle.Primary, new Emoji("✨"))
                .WithButton("Magias", $"btn_magias_{ficha.Id}", ButtonStyle.Primary, new Emoji("🧠"))
                .WithButton("Moedas", $"btn_moedas_{ficha.Id}", ButtonStyle.Secondary, new Emoji("💰"))
                .WithButton("Inventário", $"btn_inventario_{ficha.Id}", ButtonStyle.Secondary, new Emoji("🎒"))
                .WithButton("Equipamentos", $"btn_equipamentos_{ficha.Id}", ButtonStyle.Secondary, new Emoji("🗡️"));

            await ModifyOriginalResponseAsync(msg =>
            {
                msg.Embed = embed;
                msg.Components = components.Build();
            });


        }


        [ComponentInteraction("btn_idiomas_*")]
        public async Task MostrarIdiomasHandler(string fichaId)
        {
            if (!Guid.TryParse(fichaId, out var id))
            {
                await RespondAsync("❌ **ID inválido.**", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(id);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("⛔ **Acesso negado.**", ephemeral: true);
                return;
            }

            await _idiomaService.ObterFichaIdiomasAsync(ficha);

            if (ficha.Idiomas == null || ficha.Idiomas.Count == 0)
            {
                await RespondAsync($"🗣️ **{ficha.Nome}** ainda não conhece nenhum idioma.", ephemeral: true);
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle($"🗣️ Idiomas de {ficha.Nome}")
                .WithColor(Color.DarkPurple);

            var grupos = ficha.Idiomas.Select(x=>x.Idioma)
                .GroupBy(i => i.Categoria)
                .OrderBy(g => g.Key.ToString());

            foreach (var grupo in grupos)
            {
                var titulo = $"🧩 {ObterNomeCategoria(grupo.Key)}";
                var linhas = new List<string>();

                foreach (var idioma in grupo.OrderBy(i => i.Nome))
                {
                    var fonte = string.IsNullOrWhiteSpace(idioma.Fonte)
                        ? ""
                        : $"\n{idioma.Fonte} — p.{idioma.Pagina}";

                    var descricao = string.IsNullOrWhiteSpace(idioma.Descricao)
                        ? "*Sem descrição.*"
                        : idioma.Descricao;

                    linhas.Add($"**📖 {idioma.Nome.ToUpper()}**\n🔹 {descricao}{fonte}");
                }

                embed.AddField(titulo, string.Join("\n\n", linhas), inline: false);
            }

            await RespondAsync(embed: embed.Build(), ephemeral: true);
        }

        private string ObterNomeCategoria(CategoriaIdioma categoria)
        {
            return categoria switch
            {
                CategoriaIdioma.Standard => "Padrão",
                CategoriaIdioma.Exotic => "Exótico",
                CategoriaIdioma.Dialeto => "Dialeto",
                CategoriaIdioma.Secreto => "Secreto",
                CategoriaIdioma.TelepaticoOuMagico => "Telepático ou Mágico",
                CategoriaIdioma.RegionalOuCultural => "Regional ou Cultural",
                CategoriaIdioma.BestialOuPictografico => "Bestial ou Pictográfico",
                CategoriaIdioma.ArtificialOuConstruto => "Artificial ou Construto",
                _ => categoria.ToString()
            };
        }






        [ComponentInteraction("btn_proficiencias_*")]
        public async Task MostrarProficienciasHandler(string fichaId)
        {
            if (!Guid.TryParse(fichaId, out var id))
            {
                await RespondAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(id);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("❌ Acesso negado.", ephemeral: true);
                return;
            }

            if (ficha.Proficiencias == null || ficha.Proficiencias.Count == 0)
            {
                await RespondAsync("📜 Nenhuma proficiência registrada.", ephemeral: true);
                return;
            }

            var texto = string.Join(", ", ficha.Proficiencias.Select(p => p.Proficiencia.Nome));
            await RespondAsync($"📜 **Proficiencias de {ficha.Nome}:**\n{texto}", ephemeral: true);
        }


        [ComponentInteraction("btn_resistencias_*")]
        public async Task MostrarResistenciasHandler(string fichaId)
        {
            if (!Guid.TryParse(fichaId, out var id))
            {
                await RespondAsync("❌ **ID inválido.**", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(id);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("⛔ **Acesso negado.**", ephemeral: true);
                return;
            }

            await _resistenciaService.ObterFichaResistenciasAsync(ficha);

            if (ficha.Resistencias == null || ficha.Resistencias.Count == 0)
            {
                await RespondAsync($"🛡️ **{ficha.Nome}** não possui resistências registradas.", ephemeral: true);
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle($"🛡️ Resistências de {ficha.Nome}")
                .WithColor(Color.Orange);

            foreach (var resistencia in ficha.Resistencias
                .Where(r => r.Resistencia != null)
                .OrderBy(r => r.Resistencia.TipoDano.ToString()))
            {
                var tipo = resistencia.Resistencia.TipoDano.ToString().ToUpper();
                var dados = await _resistenciaService.ObterTodosResistenciasAsync();
                var info = dados.FirstOrDefault(r => r.TipoDano == resistencia.Resistencia.TipoDano);

                var nome = info?.Nome ?? tipo;
                var descricao = string.IsNullOrWhiteSpace(info?.Descricao)
                    ? "*Sem descrição.*"
                    : info.Descricao;

                var fonte = !string.IsNullOrWhiteSpace(info?.Fonte)
                    ? $"📚 {info.Fonte} — p.{info.Pagina}"
                    : null;

                var texto = $"📖 {descricao}";
                if (fonte != null)
                    texto += $"\n{fonte}";

                embed.AddField($"**🔹 {nome.ToUpper()}**", texto, inline: false);
            }

            await RespondAsync(embed: embed.Build(), ephemeral: true);
        }



        [ComponentInteraction("btn_caracteristicas_*")]
        public async Task MostrarCaracteristicasHandler(string fichaId)
        {
            if (!Guid.TryParse(fichaId, out var id))
            {
                await RespondAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(id);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("❌ Acesso negado.", ephemeral: true);
                return;
            }

            if (ficha.Caracteristicas == null || ficha.Caracteristicas.Count == 0)
            {
                await RespondAsync("✨ Nenhuma característica registrada.", ephemeral: true);
                return;
            }

            var texto = string.Join("\n", ficha.Caracteristicas.Select(c => $"• {c.Caracteristica.Nome}: {c.Caracteristica.Descricao}"));
            await RespondAsync($"✨ **Características de {ficha.Nome}:**\n{texto}", ephemeral: true);
        }


        [ComponentInteraction("btn_magias_*")]
        public async Task MostrarMagiasHandler(string fichaId)
        {
            if (!Guid.TryParse(fichaId, out var id))
            {
                await RespondAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(id);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("❌ Acesso negado.", ephemeral: true);
                return;
            }

            if (ficha.MagiasRaciais == null || ficha.MagiasRaciais.Count == 0)
            {
                await RespondAsync("🧠 Nenhuma magia racial registrada.", ephemeral: true);
                return;
            }

            var texto = string.Join("\n", ficha.MagiasRaciais.Select(m => $"• {m.Magia.Nome} ({m.Magia.Nivel}º nível): {m.Magia.Descricao}"));
            await RespondAsync($"🧠 **Magias Raciais de {ficha.Nome}:**\n{texto}", ephemeral: true);
        }


        [ComponentInteraction("btn_moedas_*")]
        public async Task MostrarMoedasHandler(string fichaId)
        {
            if (!Guid.TryParse(fichaId, out var id))
            {
                await RespondAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(id);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("❌ Acesso negado.", ephemeral: true);
                return;
            }

            if (ficha.BolsaDeMoedas?.Moedas == null || ficha.BolsaDeMoedas.Moedas.Count == 0)
            {
                await RespondAsync("💰 Nenhuma moeda registrada.", ephemeral: true);
                return;
            }

            var moedasFormatadas = ficha.BolsaDeMoedas.Moedas
                .GroupBy(m => m.Tipo)
                .Select(g => $"{ObterEmojiMoeda(g.Key)} {g.Key}: {g.Sum(m => m.Quantidade)}");

            var texto = string.Join("\n", moedasFormatadas);
            await RespondAsync($"💰 **Bolsa de moedas de {ficha.Nome}:**\n{texto}", ephemeral: true);
        }


        private string ObterEmojiMoeda(TipoMoeda tipo)
        {
            return tipo switch
            {
                TipoMoeda.PC => "🟤", // cobre
                TipoMoeda.PP => "⚪", // prata
                TipoMoeda.PE => "🟣", // eletro
                TipoMoeda.PO => "🟡", // ouro
                TipoMoeda.PL => "🔵", // platina
                _ => "❓" // fallback para valores desconhecidos

            };
        }


        [ComponentInteraction("btn_inventario_*")]
        public async Task MostrarInventarioHandler(string fichaIdStr)
        {
            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await RespondAsync("ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            // Supondo que você tenha método para buscar inventário:
            var inventario = await _inventarioService.ObterInventarioAsync(fichaId);

            if (inventario == null || !inventario.Itens.Any())
            {
                await RespondAsync("Inventário vazio.", ephemeral: true);
                return;
            }

            var textoItens = string.Join("\n", inventario.Itens.Select(i => $"{i.Quantidade}x {i.ItemBase.Nome}"));

            var embed = new EmbedBuilder()
                .WithTitle($"🎒 Inventário de {ficha.Nome}")
                .WithDescription(textoItens)
                .WithColor(Color.Gold)
                .Build();

            await RespondAsync(embed: embed, ephemeral: true);
        }


        [ComponentInteraction("btn_equipamentos_*")]
        public async Task MostrarEquipamentosHandler(string fichaIdStr)
        {
            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await RespondAsync("ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            // Supondo que você tenha método para buscar equipamentos equipados:
            var equipamentos = await _inventarioService.ObterEquipamentosEquipadosAsync(fichaId);

            if (equipamentos == null || !equipamentos.Any())
            {
                await RespondAsync("Nenhum equipamento equipado.", ephemeral: true);
                return;
            }

            var textoEquipamentos = string.Join("\n", equipamentos.Select(e => $"{e.Key}: {e.Value.ItemBase.Nome}"));

            var embed = new EmbedBuilder()
                .WithTitle($"🗡️ Equipamentos Equipados de {ficha.Nome}")
                .WithDescription(textoEquipamentos)
                .WithColor(Color.DarkGreen)
                .Build();

            await RespondAsync(embed: embed, ephemeral: true);
        }


    }
}

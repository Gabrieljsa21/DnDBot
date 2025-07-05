using Discord;
using Discord.Interactions;
using DnDBot.Application.Models.AntecedenteModels;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Bot.Helpers
{
    public static class SelectMenuHelper
    {
        public static SelectMenuBuilder CriarSelectRaca(IEnumerable<Raca> racas, string selecionado = null)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_raca")
                .WithPlaceholder("Escolha a raça");

            foreach (var raca in racas.Take(25))
            {
                AdicionarOpcaoSafe(select, raca.Nome, raca.Id, raca.Id == selecionado, raca.Descricao);
            }

            return select;
        }

        public static SelectMenuBuilder CriarSelectClasse(IEnumerable<Classe> classes, string selecionado = null)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_classe")
                .WithPlaceholder("Escolha a classe");

            foreach (var classe in classes.Take(25))
            {
                AdicionarOpcaoSafe(select, classe.Nome, classe.Id, classe.Id == selecionado, classe.Descricao);
            }

            return select;
        }

        public static SelectMenuBuilder CriarSelectAntecedente(IEnumerable<Antecedente> antecedentes, string selecionado = null)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_antecedente")
                .WithPlaceholder("Escolha o antecedente");

            foreach (var ant in antecedentes.Take(25))
            {
                AdicionarOpcaoSafe(select, ant.Nome, ant.Id, ant.Id == selecionado, ant.Descricao);
            }

            return select;
        }

        public static SelectMenuBuilder CriarSelectAlinhamento(IEnumerable<Alinhamento> alinhamentos, string selecionado = null)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_alinhamento")
                .WithPlaceholder("Escolha o alinhamento");

            foreach (var alinhamento in alinhamentos.Take(25))
            {
                AdicionarOpcaoSafe(select, alinhamento.Nome, alinhamento.Id, alinhamento.Id == selecionado, alinhamento.Descricao);
            }

            return select;
        }

        public static SelectMenuBuilder CriarSelectSubraca(IEnumerable<SubRaca> subRacas, string selecionado = null)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_subraca")
                .WithPlaceholder("Escolha a sub-raça");

            foreach (var subRaca in subRacas.Take(25))
            {
                AdicionarOpcaoSafe(select, subRaca.Nome, subRaca.Id, subRaca.Id == selecionado, subRaca.Descricao);
            }

            return select;
        }

        private static void AdicionarOpcaoSafe(
            SelectMenuBuilder select,
            string label,
            string value,
            bool selecionado = false,
            string descricao = null)
        {
            if (string.IsNullOrWhiteSpace(label) || string.IsNullOrWhiteSpace(value))
                return;

            label = label.Trim();
            value = value.Trim();

            if (label.Length < 1 || value.Length < 1)
                return;

            label = label.Length > 100 ? label.Substring(0, 100) : label;
            value = value.Length > 25 ? value.Substring(0, 25) : value;
            descricao = descricao?.Length > 100 ? descricao.Substring(0, 100) : descricao;

            var option = new SelectMenuOptionBuilder()
                .WithLabel(label)
                .WithValue(value)
                .WithDefault(selecionado);

            if (!string.IsNullOrWhiteSpace(descricao))
                option.WithDescription(descricao);

            select.AddOption(option);
        }

    }
}

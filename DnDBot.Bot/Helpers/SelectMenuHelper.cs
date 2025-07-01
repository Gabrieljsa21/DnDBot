using Discord;
using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Bot.Helpers
{
    /// <summary>
    /// Classe auxiliar responsável por gerar os menus suspensos (select menus) usados durante a criação de fichas.
    /// </summary>
    public static class SelectMenuHelper
    {
        /// <summary>
        /// Cria o menu de seleção para Raças.
        /// </summary>
        /// <param name="racas">Lista de raças disponíveis.</param>
        /// <returns>Menu de seleção configurado com até 25 raças.</returns>
        public static SelectMenuBuilder CriarSelectRaca(IEnumerable<Raca> racas)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_raca")
                .WithPlaceholder("Escolha a raça");

            foreach (var raca in racas.Take(25))
            {
                string value = raca.Nome.Length > 25 ? raca.Nome.Substring(0, 25) : raca.Nome;
                select.AddOption(raca.Nome, value);
            }

            return select;
        }

        /// <summary>
        /// Cria o menu de seleção para Classes.
        /// </summary>
        /// <param name="classes">Lista de classes disponíveis.</param>
        /// <returns>Menu de seleção configurado com até 25 classes.</returns>
        public static SelectMenuBuilder CriarSelectClasse(IEnumerable<string> classes)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_classe")
                .WithPlaceholder("Escolha a classe");

            foreach (var classe in classes.Take(25))
            {
                string value = classe.Length > 25 ? classe.Substring(0, 25) : classe;
                select.AddOption(classe, value);
            }

            return select;
        }

        /// <summary>
        /// Cria o menu de seleção para Antecedentes.
        /// </summary>
        /// <param name="antecedentes">Lista de antecedentes disponíveis.</param>
        /// <returns>Menu de seleção configurado com até 25 antecedentes.</returns>
        public static SelectMenuBuilder CriarSelectAntecedente(IEnumerable<string> antecedentes)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_antecedente")
                .WithPlaceholder("Escolha o antecedente");

            foreach (var ant in antecedentes.Take(25))
            {
                string value = ant.Length > 25 ? ant.Substring(0, 25) : ant;
                select.AddOption(ant, value);
            }

            return select;
        }

        /// <summary>
        /// Cria o menu de seleção para Alinhamentos.
        /// </summary>
        /// <param name="alinhamentos">Lista de alinhamentos disponíveis.</param>
        /// <returns>Menu de seleção configurado com até 25 alinhamentos.</returns>
        public static SelectMenuBuilder CriarSelectAlinhamento(IEnumerable<string> alinhamentos)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_alinhamento")
                .WithPlaceholder("Escolha o alinhamento");

            foreach (var ali in alinhamentos.Take(25))
            {
                string value = ali.Length > 25 ? ali.Substring(0, 25) : ali;
                select.AddOption(ali, value);
            }

            return select;
        }

        /// <summary>
        /// Cria um menu de seleção de sub-raças para interação com o usuário no Discord.
        /// Cada opção do menu corresponde a uma sub-raça disponível para a raça selecionada.
        /// </summary>
        /// <param name="subRacas">Coleção de sub-raças disponíveis para escolha.</param>
        /// <returns>
        /// Um <see cref="SelectMenuBuilder"/> configurado com até 25 opções de sub-raças.
        /// </returns>
        public static SelectMenuBuilder CriarSelectSubraca(IEnumerable<SubRaca> subRacas)
        {
            var select = new SelectMenuBuilder()
                .WithCustomId("select_subraca")
                .WithPlaceholder("Escolha a sub-raça");

            foreach (var subRaca in subRacas.Take(25))
            {
                string value = subRaca.Nome.Length > 25 ? subRaca.Nome.Substring(0, 25) : subRaca.Nome;
                select.AddOption(subRaca.Nome, value);
            }

            return select;
        }


    }
}

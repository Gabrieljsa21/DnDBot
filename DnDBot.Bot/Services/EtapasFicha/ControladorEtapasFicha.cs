using Discord.Interactions;
using DnDBot.Bot.Models.Ficha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public class ControladorEtapasFicha
    {
        private readonly List<IEtapaFicha> _etapas;

        public ControladorEtapasFicha(IEnumerable<IEtapaFicha> etapas)
        {
            _etapas = etapas.ToList();
        }

        public async Task<bool> ProcessarProximaEtapaAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
        {
            foreach (var etapa in _etapas)
            {
                Console.WriteLine($"[Etapa] {etapa.GetType().Name}: completa = {await etapa.EstaCompletaAsync(ficha)}");

                if (!await etapa.EstaCompletaAsync(ficha))
                {
                    await etapa.ExecutarAsync(ficha, context, usarFollowUp);
                    return true;
                }
            }

            return false; // Todas etapas completadas
        }

        // Validador estático para centralizar regra de ficha completa
        public static class ValidadorFicha
        {
            private static readonly string[] ValoresInvalidos = { "Não definida", "Não definido", null, "" };

            public static bool EstaCompleta(FichaPersonagem ficha)
            {
                if (ficha == null) return false;
                if (string.IsNullOrWhiteSpace(ficha.Nome)) return false;

                return
                    !ValoresInvalidos.Contains(ficha.RacaId) &&
                    !ValoresInvalidos.Contains(ficha.ClasseId) &&
                    !ValoresInvalidos.Contains(ficha.AntecedenteId) &&
                    !ValoresInvalidos.Contains(ficha.AlinhamentoId);
            }
        }
    }
}

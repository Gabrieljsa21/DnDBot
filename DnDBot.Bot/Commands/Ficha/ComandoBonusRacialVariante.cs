using Discord;
using Discord.Interactions;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.EtapasFicha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    public class ComandoBonusRacialVariante : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private static readonly Dictionary<ulong, (string Mais2, string Mais1)> _bonusTemporarios = new();
        private readonly ControladorEtapasFicha _controladorEtapasFicha;

        public ComandoBonusRacialVariante(FichaService fichaService, ControladorEtapasFicha controladorEtapasFicha)
        {
            _fichaService = fichaService;
            _controladorEtapasFicha = controladorEtapasFicha;
        }

        [ComponentInteraction("bonus_atributo_2_*")]
        public async Task BonusDoisHandler(string fichaIdStr, string[] selected)
        {
            await DeferAsync(ephemeral: true);

            if (!Guid.TryParse(fichaIdStr, out var fichaId) || selected.Length == 0)
            {
                await FollowupAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var atributo2 = selected.First();
            var key = Context.User.Id;

            if (!_bonusTemporarios.ContainsKey(key))
                _bonusTemporarios[key] = (null, null);

            _bonusTemporarios[key] = (atributo2, _bonusTemporarios[key].Mais1);

            await VerificarSePodeSalvarBonus(fichaId);
        }

        [ComponentInteraction("bonus_atributo_1_*")]
        public async Task BonusUmHandler(string fichaIdStr, string[] selected)
        {
            await DeferAsync(ephemeral: true);

            if (!Guid.TryParse(fichaIdStr, out var fichaId) || selected.Length == 0)
            {
                await FollowupAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var atributo1 = selected.First();
            var key = Context.User.Id;

            if (!_bonusTemporarios.ContainsKey(key))
                _bonusTemporarios[key] = (null, null);

            _bonusTemporarios[key] = (_bonusTemporarios[key].Mais2, atributo1);

            await VerificarSePodeSalvarBonus(fichaId);
        }

        private async Task VerificarSePodeSalvarBonus(Guid fichaId)
        {
            var key = Context.User.Id;

            if (!_bonusTemporarios.TryGetValue(key, out var bonus) || bonus.Mais2 == null || bonus.Mais1 == null)
                return;

            if (bonus.Mais2 == bonus.Mais1)
            {
                await FollowupAsync("❌ Você deve escolher dois atributos **diferentes** para +2 e +1.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null)
            {
                await FollowupAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            ficha.BonusAtributos.Add(new BonusAtributo
            {
                Id = Guid.NewGuid().ToString(),
                Atributo = Enum.Parse<Atributo>(bonus.Mais2),
                Valor = 2,
                Origem = "VarianteCustomBonus",
                OwnerType = "FichaPersonagem",
                OwnerId = ficha.Id
            });

            ficha.BonusAtributos.Add(new BonusAtributo
            {
                Id = Guid.NewGuid().ToString(),
                Atributo = Enum.Parse<Atributo>(bonus.Mais1),
                Valor = 1,
                Origem = "VarianteCustomBonus",
                OwnerType = "FichaPersonagem",
                OwnerId = ficha.Id
            });

            if (!ficha.Tags.Contains("CustomBonus"))
            {
                ficha.Tags = ficha.Tags.Append("CustomBonus").ToList();
            }

            await _fichaService.AtualizarFichaAsync(ficha);
            _bonusTemporarios.Remove(key);

            await FollowupAsync("✅ Bônus aplicados com sucesso!", ephemeral: true);

            await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: true);
        }
    }
}

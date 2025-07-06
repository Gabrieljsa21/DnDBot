using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models.Inventario
{
    namespace DnDBot.Application.Models.Inventario
    {
        /// <summary>
        /// Define os slots de equipamento disponíveis para personagens.
        /// </summary>
        public enum SlotEquipamento
        {
            /// <summary>Mão principal (ex: espada, machado).</summary>
            MaoPrincipal,

            /// <summary>Mão secundária (ex: escudo, adaga).</summary>
            MaoSecundaria,

            /// <summary>Ambas as mãos (ex: arco longo, espada grande).</summary>
            AmbasAsMaos,

            /// <summary>Armadura corporal principal (leve, média ou pesada).</summary>
            ArmaduraCorpo,

            /// <summary>Escudo ou proteção adicional.</summary>
            Escudo,

            /// <summary>Capacete, elmo ou tiara.</summary>
            Cabeca,

            /// <summary>Luvas ou manoplas.</summary>
            Maos,

            /// <summary>Botas ou calçados.</summary>
            Pes,

            /// <summary>Capa, manto ou asas.</summary>
            Costas,

            /// <summary>Anel mágico (1º slot).</summary>
            Anel1,

            /// <summary>Anel mágico (2º slot).</summary>
            Anel2,

            /// <summary>Amuleto, colar ou talismã.</summary>
            Pescoco,

            /// <summary>Faixa, cinturão ou cinto mágico.</summary>
            Cintura
        }
    }

}

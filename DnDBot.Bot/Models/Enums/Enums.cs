using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Enums
{

    public enum EtapaCriacaoFicha
    {
        Inicio = 1,
        RacaClasseAntecedenteAlinhamento = 2,
        Subraca = 3,
        DistribuicaoAtributos = 4,
        BonusAtributos = 5,
        Idiomas = 6
    }

    /// <summary>
    /// Enumeração que representa os atributos básicos de um personagem em Dungeons & Dragons.
    /// Cada valor corresponde a uma característica fundamental que influencia as habilidades e testes do personagem.
    /// </summary>
    public enum TipoRolagem
    {
        /// <summary>
        /// Rolagem normal, sem vantagem ou desvantagem.
        /// </summary>
        Normal,

        /// <summary>
        /// Rolagem com vantagem (melhor resultado entre dois lançamentos).
        /// </summary>
        Vantagem,

        /// <summary>
        /// Rolagem com desvantagem (pior resultado entre dois lançamentos).
        /// </summary>
        Desvantagem
    }
    
    public enum Atributo
    {
        /// <summary>Representa a Força física do personagem, usada para força bruta e ataques corpo a corpo.</summary>
        Forca,

        /// <summary>Representa a agilidade, reflexos e destreza manual.</summary>
        Destreza,

        /// <summary>Representa a saúde, resistência física e vigor.</summary>
        Constituicao,

        /// <summary>Representa o raciocínio, memória e conhecimento do personagem.</summary>
        Inteligencia,

        /// <summary>Representa a percepção, intuição e força de vontade.</summary>
        Sabedoria,

        /// <summary>Representa o carisma, influência e capacidade social.</summary>
        Carisma,

        Selecionável
    }


    /// <summary>
    /// Representa os tipos de moedas usadas em Dungeons & Dragons.
    /// </summary>
    public enum TipoMoeda
    {
        /// <summary>Peça de Cobre, a unidade base.</summary>
        PC,

        /// <summary>Peça de Prata.</summary>
        PP,

        /// <summary>Peça de Electrum.</summary>
        PE,

        /// <summary>Peça de Ouro.</summary>
        PO,

        /// <summary>Peça de Platina.</summary>
        PL
    }

    public enum TamanhoCriatura
    {
        Desconhecido = 0,
        Minúsculo,
        Pequeno,
        Médio,
        Grande,
        Enorme,
        Colossal
    }


    public enum TipoDano
    {
        /// <summary>Dano causado por armas cortantes, como espadas e machados.</summary>
        Cortante,

        /// <summary>Dano causado por armas perfurantes, como lanças e flechas.</summary>
        Perfurante,

        /// <summary>Dano causado por impacto contundente, como martelos e porrada.</summary>
        Contundente,

        /// <summary>Dano causado por fogo ou calor intenso.</summary>
        Fogo,

        /// <summary>Dano causado por frio intenso ou gelo.</summary>
        Gelo,

        /// <summary>Dano causado por eletricidade ou choque elétrico.</summary>
        Elétrico,

        /// <summary>Dano causado por ácido ou substâncias corrosivas.</summary>
        Ácido,

        /// <summary>Dano causado por veneno ou toxinas.</summary>
        Veneno,

        /// <summary>Dano causado por força mágica ou física não convencional.</summary>
        Força,

        /// <summary>Dano causado por energia radiante, geralmente associada a forças divinas.</summary>
        Radiante,

        /// <summary>Dano causado por energia necromântica ou força negativa.</summary>
        Necrótico,

        /// <summary>Dano psicológico ou mental, como medo ou confusão.</summary>
        Psíquico
    }

    public enum TipoArma
    {
        CorpoACorpo,
        ADistancia
    }

    /// <summary>
    /// Slots onde equipamentos podem ser equipados.
    /// </summary>
    public enum SlotEquipamento
    {
        MaoDireita,
        MaoEsquerda,
        Cabeca,
        Corpo,
        Pes,
    }

    public enum CategoriaArma
    {
        Simples,
        Marcial
    }

    /// <summary>
    /// Enumeração que define os tipos de armadura.
    /// </summary>
    public enum TipoArmadura
    {
        /// <summary>Armadura leve (ex: gibão de couro).</summary>
        Leve,

        /// <summary>Armadura média (ex: cota de escamas).</summary>
        Media,

        /// <summary>Armadura pesada (ex: armadura completa).</summary>
        Pesada,

        /// <summary>Escudo (complemento defensivo).</summary>
        Escudo
    }

    public enum AnatomiaPermitida
    {
        Humanoide,
        Quadrupede,
        Asas,
        Cauda,
        Tentaculo,
        // etc.
    }
    public enum CategoriaIdioma
    {
        Standard,
        Exotic,
        Dialeto,
        Secreto,
        TelepaticoOuMagico,
        RegionalOuCultural,
        BestialOuPictografico,
        ArtificialOuConstruto
    }

    public enum TipoCaracteristica
    {
        Passiva,
        Ativa,
        Reacao,
        Resistência,
        Imunidade,
        Magia,
        HabilidadeEspecial,
        PericiaExtra,
        Outra
    }

    public enum AcaoRequerida
    {
        Nenhuma,
        Acao,
        AcaoBonus,
        Reacao,
        Movimento,
        TempoLivre
    }

    public enum Alvo
    {
        Self,
        Aliado,
        Inimigo,
        Area,
        Objeto,
        Criatura
    }

    public enum CondicaoAtivacao
    {
        Sempre,
        EmCombate,
        EmAmbienteNatural,
        AposAtaque,
        SobDano,
        AposMagia,
        EmDescanso
    }

    public enum TipoEfeito
    {
        BonusAtributo,
        ResistenciaDano,
        ImunidadeDano,
        VulnerabilidadeDano,
        Condicao,
        ModificadorAtaque,
        ModificadorDefesa,
        Velocidade,
        Outros
    }

    public enum Condicao
    {
        Atordoado,
        Enfeitiçado,
        Paralisado,
        Cego,
        Surdo,
        Imobilizado,
        Silenciado,
        Envenenado,
        Caído,
        Outros
    }

    public enum OrigemCaracteristica
    {
        Racial,
        Classe,
        Feat,
        MagiaRacial,
        ItemMagico,
        Outra
    }
}

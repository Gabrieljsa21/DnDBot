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

    public enum TipoNarrativa
    {
        Ideal,
        Vinculo,
        Defeito
    }

    /// <summary>
    /// Enumeração para indicar o tipo de cura aplicada por uma poção.
    /// </summary>
    public enum TipoCura
    {
        /// <summary>Valor numérico fixo (ex: 10 PV).</summary>
        Fixo,

        /// <summary>Rolagem de dado (ex: 2d4+2).</summary>
        Dado,

        /// <summary>Porcentagem do total de PV.</summary>
        Porcentagem
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
        Psíquico,
        Nenhum
    }

    public enum TipoArma
    {
        CorpoACorpo,
        Distancia
    }

    /// <summary>
    /// Slots onde equipamentos podem ser equipados.
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

    public enum CategoriaArma
    {
        Simples,
        Marcial
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

    public enum Alvo
    {
        Self,
        Aliado,
        Inimigo,
        Area,
        Objeto,
        Criatura
    }

    public enum FormaAreaEfeito
    {
        Cone,
        Esfera,
        Cilindro,
        Cubo,
        Linha,
        Nuvem,
        Nenhum,
        Outro
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
        Enfeiticado,
        Paralisado,
        Cego,
        Surdo,
        Imobilizado,
        Silenciado,
        Envenenado,
        Caido,
        Outros
    }

    public enum OrigemCaracteristica
    {
        Racial,
        Classe,
        Antecedente,
        Feat,
        MagiaRacial,
        ItemMagico,
        Outra
    }
    public enum TipoProficiencia
    {
        Pericia,
        Ferramenta,
        Arma,
        Armadura,
        Idioma,
        Veiculo,
        TesteResistencia
    }

    public enum NivelMagia
    {
        Truque = 0,
        Nivel1 = 1,
        Nivel2 = 2,
        Nivel3 = 3,
        Nivel4 = 4,
        Nivel5 = 5,
        Nivel6 = 6,
        Nivel7 = 7,
        Nivel8 = 8,
        Nivel9 = 9
    }

    public enum EscolaMagia
    {
        Abjuracao,
        Conjuracao,
        Adivinhacao,
        Encantamento,
        Evocacao,
        Ilusao,
        Necromancia,
        Transmutacao
    }

    public enum TipoUsoMagia
    {
        Acao,
        AcaoBonus,
        Reacao,
        Minuto1,
        Minutos10,
        Hora1,
        TempoEspecial
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

    public enum RecargaMagia
    {
        Nenhuma,
        DescansoCurto,
        DescansoLongo,
        Diario,
        PorCombate,
        Personalizada
    }

    public enum DuracaoUnidade
    {
        Instantanea,
        Rodadas,
        Minutos,
        Horas,
        Permanente,
        Especial
    }

    public enum ClassePersonagem
    {
        Barbaro,
        Bardo,
        Bruxo,
        Clerigo,
        Druida,
        Feiticeiro,
        Guerreiro,
        Ladino,
        Mago,
        Monge,
        Paladino,
        Patrulheiro,
        //SangueRuim,
        Artifice,
        Outra
    }

    public enum TipoAlcance
    {
        Toque,
        Pessoal,
        Vista,
        Metros1,    // ~5 pés
        Metros3,    // ~10 pés
        Metros4,    // ~15 pés
        Metros9,    // ~30 pés
        Metros18,   // ~60 pés
        Metros27,   // ~90 pés
        Metros36,   // ~120 pés
        Ilimitado,
        Especial
    }

    /// <summary>
    /// Enum que representa as possíveis categorias de itens no sistema D&D 5e.
    /// </summary>
    public enum CategoriaItem
    {
        Arma,
        Armadura,
        Escudo,
        Ferramenta,
        Consumivel,
        EquipamentoAventura,
        Roupas,
        Veiculo,
        LivroOuTomo,
        Outro
    }

    public enum SubcategoriaItem
    {
        Nenhuma,

        // Para Consumíveis
        Pocao,
        Pergaminho,
        Alimento,
        Bebida,
        Kit,

        // Para Ferramentas
        FerramentaCarpinteiro,
        FerramentaCartografo,
        FerramentaCostureiro,
        FerramentaCoureiro,
        FerramentaEntalhador,
        FerramentaFerreiro,
        FerramentaFunileiro,
        FerramentaJoalheiro,
        FerramentaOleiro,
        FerramentaPedreiro,
        FerramentaPintor,
        FerramentaSapateiro,
        FerramentaVidreiro,
        FerramentaLadrao,
        FerramentaNavegador,

        // Para Armas
        ArmaCorpoACorpo,
        ArmaADistancia,

        // Para Armaduras
        ArmaduraLeve,
        ArmaduraMedia,
        ArmaduraPesada,
        Escudo,

        // Para outros
        InstrumentoMusical,
        ObjetoMagico,
        EquipamentoAventura,
        Roupas,
        Veiculo,
        LivroOuTomo,
        Joia,
        AnimalEstimacao,
        Documento,
        Mapa,
        Fetiche,
        Insignia,
        Amuleto,

        Outro
    }

    public enum RaridadeItem
    {
        Comum, Incomum, Rara, MuitoRara, Lendária, Artefato
    }

}

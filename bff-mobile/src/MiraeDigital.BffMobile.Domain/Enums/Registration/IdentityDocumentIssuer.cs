using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    public enum IdentityDocumentIssuer
    {
        [Description("DESCONHECIDO")]
        Unknown = 0,

        [Description("DEPARTAMENTO DE TRÂNSITO")]
        [Display(Name = "DETRAN")]
        Dentran = 1,

        [Description("INTISTUTO FELIX PACHECO")]
        [Display(Name = "IFP")]
        InstitutoFelixPacheco = 2,

        [Description("INTISTUTO PEREIRA FAUSTINO")]
        [Display(Name = "IPF")]
        InstitutoPereiraFaustino = 3,

        [Description("POLICIA CIVIL")]
        [Display(Name = "PC")]
        PoliciaCivil = 4,

        [Description("POLICIA FEDERAL")]
        [Display(Name = "DPF")]
        PoliciaFederal = 5,

        [Description("POLICIA MILITAR")]
        [Display(Name = "PM")]
        PoliciaMilitar = 6,

        [Description("POLICIA TÉCNICO-CIENTÍFICA")]
        [Display(Name = "PTC")]
        PoliciaTecnicoCientifica = 7,

        [Description("SECRETARIA DA JUSTIÇA E DA SEGURANÇA")]
        [Display(Name = "SJS")]
        SecretariaJusticaoSeguranca = 8,

        [Description("SECRETARIA DA DEFESA SOCIAL")]
        [Display(Name = "SDS")]
        SecretariaDefesaSocial = 9,

        [Description("SECRETARIA DE ESTADO E SEGURANÇA PÚBLICA")]
        [Display(Name = "SESP")]
        SecretariaEstadoSegurancaPublica = 10,

        [Description("SECRETARIA DE SEGURANÇA PUBLICA")]
        [Display(Name = "SSP")]
        SecretariaSeguracaPublica = 11,

        [Description("DEPARTAMENTO POLÍCIA FEDERAL")]
        DepartamentoPoliciaFederal = 12,
        [Description("MINISTÉRIO DA JUSTIÇA")]
        MinisterioDaJustica = 13,
        [Description("MINISTÉRIO DA AERONÁUTICA")]
        MinisterioDaAeronautica = 14,
        [Description("MINISTÉRIO DA MARINHA")]
        MinisterioDaMarinha = 15,
        [Description("MINISTÉRIO DO EXÉRCITO")]
        MinisterioDoExercito = 16,
        [Description("CONS. REG. ADMNISTRAÇÃO")]
        ConsRegAdministracao = 17,
        [Description("CONS. REG. ASSISTENTES SOCIAIS")]
        ConsRegAssistentesSociais = 18,
        [Description("CONS. REG. BIBLIOTECONOMIA")]
        ConsRegBiblioteconomia = 19,
        [Description("CONS. REG. CONTABILIDADE")]
        ConsRegContabilidade = 20,
        [Description("CONS. REG. CORRETORES DE IMÓVEIS")]
        ConsRegCorretoresImoveis = 21,
        [Description("CONS. REG. DE EDUCAÇÃO FÍSICA")]
        ConsRegEducacaoFisica = 22,
        [Description("CONS. DE PROFISSIONAIS LIBERAIS")]
        ConsRegProfissionaisLiberais = 23,
        [Description("CONS. REG. ECONOMIA")]
        ConsRegEconomia = 24,
        [Description("CONS. REG. ENFERMAGEM")]
        ConsRegEnfermagem = 25,
        [Description("CONS. REG. ENGENHARIA E ARQUITETURA")]
        ConsRegEngenhariaArquitetura = 26,
        [Description("CONS. REG. FARMÁCIA")]
        ConsRegFarmacia = 27,
        [Description("CONS. REG. MEDICINA")]
        ConsRegMedicina = 28,
        [Description("CONS. REG. MEDICINA VETERINÁRIA")]
        ConsRegMedicinaVeterinaria = 29,
        [Description("CONS. REG. NUTRICIONISTA")]
        ConsRegNutricionista = 30,
        [Description("CONS. REG. ODONTOLOGIA")]
        ConsRegOdontologia = 31,
        [Description("CONS. REG. PSICOLOGIA")]
        ConsRegPsicologia = 32,
        [Description("CONS. REG. QUIMICA")]
        ConsRegQuimica = 33,
        [Description("CONS. REG. RELAÇÕES PUBLICAS")]
        ConsRegRelacoesPublicas = 34,
        [Description("CONS. REG. REPRESENTANTES COMERCIAIS")]
        ConsRegRepresentantesComerciais = 35,
        [Description("ORDEM DOS ADVOGADOS DO BRASIL")]
        OrdemDosAdvogadosDoBrasil = 36,
        [Description("CARTÓRIO")]
        Cartorio = 37,

        [Description("(Obsolete // Don't use)")]
        CAR = 38, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CCI = 39, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CEN = 40, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CH = 41, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CMV = 42, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CNH = 43, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRA = 44, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRC = 45, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRE = 46, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CREA = 47, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRF = 48, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRM = 49, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRO = 50, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRP = 51, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRQ = 52, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRRC = 53, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        CRRP = 54, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        DIC = 55, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        DNT = 56, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        MA = 57, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        MAER = 58, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        ME = 59, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        MJ = 60, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        OAB = 61, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        RNE = 62, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        SJT = 63, // Obsoletos - Description:

        [Description("(Obsolete // Don't use)")]
        SPTC = 64 // Obsoletos - Description:
    }
}

using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    public enum IdentityDocumentType
    {
        [Description("Tipo de Documento sem definição")]
        None = 0,

        [Description("RG-REGISTRO GERAL")]
        GeneralRegistration = 1,

        [Description("CARTEIRA DE HABILITAÇÃO")]
        DriverPermit = 2,

        [Description("CARTEIRA DE IDENTIDADE PARA ESTRANGEIRO")]
        ForeignerRegistration = 3,

        [Description("(Obsolete // Don't use) OAB - ORDEM DOS ADVOGADOS DO BRASIL")]
        AD = 4,

        [Description("(Obsolete // Don't use) MINISTERIO  DA AERONAUTICA")]
        AE = 5,

        [Description("(Obsolete // Don't use) CRTA - CONSELHO REG. TECNICOS DE ADMIN.")]
        AM = 6,

        [Description("(Obsolete // Don't use) CARTEIRA IDENT.EXPEDIDA FORÇAS ARMADAS")]
        AR = 7,

        [Description("(Obsolete // Don't use) CRESS  - CONS. REG. DE ASSIST. SOCIAIS")]
        AS = 8,

        [Description("(Obsolete // Don't use) CRB - CONS. REG. DE BIBLIOTECONOMIA")]
        BT = 9,

        [Description("(Obsolete // Don't use) CRECI - CONS. REG. DE CORRET. DE IMOVEIS")]
        CI = 10,

        [Description("(Obsolete // Don't use) CRC - CONSELHO REG. DE CONTABILIDADE")]
        CT = 11,

        [Description("(Obsolete // Don't use) CRE - CONSELHO REG. DE ECONOMIA")]
        EC = 12,

        [Description("(Obsolete // Don't use) COREN - CONSELHO REG. DE ENFERMAGEM")]
        EF = 13,

        [Description("(Obsolete // Don't use) CREA - CONS. REG. ENGEN. E ARQUITETURA")]
        EN = 14,

        [Description("(Obsolete // Don't use) MINISTERIO DO EXERCITO")]
        EX = 15,

        [Description("(Obsolete // Don't use) CRF - CONSELHO REG. DE FARMACIA")]
        FA = 16,

        [Description("(Obsolete // Don't use) CARTEIRA DE IDENTIDADE MILITAR")]
        IM = 17,

        [Description("(Obsolete // Don't use) CARTEIRA DE IDENTIDADE PROFISSIONAL")]
        IP = 18,

        [Description("(Obsolete // Don't use) CARTEIRA IDENTIDADE DE JUÍZES")]
        JZ = 19,

        [Description("(Obsolete // Don't use) CRQ - CONSELHO REG. DE QUIMICA")]
        KS = 20,

        [Description("(Obsolete // Don't use) CRM - CONSELHO REG. DE MEDICINA")]
        MD = 21,

        [Description("(Obsolete // Don't use) MINISTERIO DAS RELACOES EXTERIORES")]
        ME = 22,

        [Description("(Obsolete // Don't use) MINISTERIO DA JUSTICA")]
        MJ = 23,

        [Description("(Obsolete // Don't use) MINISTERIO DA MARINHA")]
        MR = 24,

        [Description("(Obsolete // Don't use) CRMV - CONS. REG. DE MED. VETERINARIA")]
        MV = 25,

        [Description("(Obsolete // Don't use) CRN - CONSELHO REG. DE NUTRICIONISTA")]
        NT = 26,

        [Description("(Obsolete // Don't use) CRO - CONSELHO REG. DE ODONTOLOGIA")]
        OD = 27,

        [Description("(Obsolete // Don't use) CARTEIRA IDENTIDADE POLICIA FEDERAL")]
        PF = 28,

        [Description("(Obsolete // Don't use) CART.IDENT.EXP.CONSELHO PROF.LIBERAIS")]
        PL = 29,

        [Description("(Obsolete // Don't use) PASSAPORTE")]
        PP = 30,

        [Description("(Obsolete // Don't use) CRP - CONSELHO REG.DE PSICOLOGIA")]
        PS = 31,

        [Description("(Obsolete // Don't use) CORE - CONS. REG. DE REPRES. COMERCIAIS")]
        RC = 32,

        [Description("(Obsolete // Don't use) CONRERP - CONS. REG. PROF. REL. PUBLICAS")]
        RP = 33
    }
}

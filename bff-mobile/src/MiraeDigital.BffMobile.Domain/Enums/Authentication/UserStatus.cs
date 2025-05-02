using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Authentication
{
    public enum UserStatus
    {
        [Description("Usuário Ativo")]
        Active = 1,

        [Description("Cadastro cliente em progresso")]
        RegistrationInProgress = 2,

        [Description("Erro nos dados do usuário.")]
        WithError = 3,

        [Description("Usuário bloqueado.")]
        Blocked = 4,

        [Description("Usuário não encontrado.")]
        NotFound = 5,

        [Description("Usuário deletado")]
        Deleted = 6
    }
}

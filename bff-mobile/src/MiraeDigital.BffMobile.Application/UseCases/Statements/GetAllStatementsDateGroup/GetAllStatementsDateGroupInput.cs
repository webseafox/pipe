using MiraeDigital.Account.Client.Http.Enum;
using MiraeDigital.Lib.Application.UseCases;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiraeDigital.BffMobile.Application.UseCases.Statements.GetAllStatementsDateGroup
{
    public sealed class GetAllStatementsDateGroupInput : IUseCaseInput
    {
        [Required]
        public DateTime RegisterFrom { get; set; }
        [Required]
        public DateTime RegisterTo { get; set; }
        [Required]
        public StatementType StatementType { get; set; }
        public string Description { get; set; }
    }
}

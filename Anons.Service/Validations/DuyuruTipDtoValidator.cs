using Anons.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Validations
{
    public class DuyuruTipDtoValidator : AbstractValidator<DuyuruTipDto>
    {
        public DuyuruTipDtoValidator()
        {
            RuleFor(s => s.DuyuruTipAdi).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}

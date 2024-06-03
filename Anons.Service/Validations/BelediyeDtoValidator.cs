using Anons.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Validations
{
    public class BelediyeDtoValidator : AbstractValidator<BelediyeDto>
    {
        public BelediyeDtoValidator()
        {
            RuleFor(s => s.BelediyeAdi).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}

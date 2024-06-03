using Anons.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Validations
{
    public class MenuDtoValidator : AbstractValidator<MenuDto>
    {
        public MenuDtoValidator()
        {
            RuleFor(s => s.MenuName).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(s => s.MenuCode).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater");
            RuleFor(s => s.TopMenuCode).InclusiveBetween(0, int.MaxValue).WithMessage("{PropertyName} must be greater");
        }
    }
}

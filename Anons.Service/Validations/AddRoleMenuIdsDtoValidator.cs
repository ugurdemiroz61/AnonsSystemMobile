using Anons.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Validations
{
    public class AddRoleMenuIdsDtoValidator : AbstractValidator<AddRoleMenuIdsDto>
    {
        public AddRoleMenuIdsDtoValidator()
        {
            RuleFor(s => s.RoleId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(s => s.MenuId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}

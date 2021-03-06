﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.Systems
{
    public class RoleCreateRequestValidatior : AbstractValidator<RoleCreateRequest>
    {
        public RoleCreateRequestValidatior()
        {
            RuleFor(role => role.Id).NotEmpty().WithMessage("Id value is required.")
                .MaximumLength(50).WithMessage("Role id cannot over limit 50 characters.");
            RuleFor(role => role.Name).NotEmpty().WithMessage("Role name is required.");
        }
    }
}

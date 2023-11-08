using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

using Imtudou.Core.Utility.IDCard;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Application.Authorization.Accounts.Dto
{
    public class RegisterInputValidator : AbstractValidator<RegisterInput>
    {
        public RegisterInputValidator()
        {
            RuleFor(s => s.Phone).NotEmpty().WithMessage($"{nameof(RegisterInput.Phone)} 必填!");
            RuleFor(s => s.IDCard).NotEmpty().NotEmpty().WithMessage($"{nameof(RegisterInput.IDCard)} 必填!");
            RuleFor(s => s.IDCard.ToLower()).Equal("string").WithMessage($"{nameof(RegisterInput.IDCard)} 必填!");
            RuleFor(s => s.IDCard).IsIDCard();
        }
    }


    public static class IDCardExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> IsIDCard<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new IDCardValidator<T, TProperty>());
        }
    }

    public class IDCardValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        private string _error = string.Empty;
        public override string Name => "IDCardValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            if (value.ToString() == true.ToString())
            {
                var t = context.InstanceToValidate as RegisterInput;
                if (!IDCardCheck.Check(t.IDCard))
                {
                    this._error = "身份证不符合GB11643-1999标准!";
                    return false;
                }
            }
            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
          => this._error;
    }
}

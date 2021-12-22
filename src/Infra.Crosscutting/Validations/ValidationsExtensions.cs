using FluentValidation;
using Vantage.Infra.Crosscutting.Validations.Validators;

namespace Vantage.Infra.Crosscutting.Validations
{
    public static class ValidationsExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidCnpj<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CnpjValidator<T>());
        public static IRuleBuilderOptions<T, string> IsValidCpf<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CpfValidator<T>());
    }
}

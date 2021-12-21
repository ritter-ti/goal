using System;
using Ritter.Infra.Crosscutting;
using Ritter.Infra.Crosscutting.Specifications;

namespace Ritter.Domain.Business
{
    public abstract class BusinessRule<TEntity> : IBusinessRule<TEntity>
        where TEntity : class
    {
        private readonly ISpecification<TEntity> rule;
        private readonly Action<TEntity> action;

        protected BusinessRule(ISpecification<TEntity> rule, Action<TEntity> action)
        {
            Ensure.Argument.NotNull(rule, nameof(rule), $"Please provide a valid non null {nameof(rule)} delegate instance.");
            Ensure.Argument.NotNull(action, nameof(action), $"Please provide a valid non null {nameof(action)} delegate instance.");

            this.rule = rule;
            this.action = action;
        }

        public void Evaluate(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity), "Cannot evaulate a business rule against a null reference.");

            if (rule.IsSatisfiedBy(entity))
            {
                action(entity);
            }
        }
    }
}

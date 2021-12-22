using FluentAssertions;
using Vantage.Infra.Crosscutting.Specifications;
using Vantage.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Specifications
{
    public class DirectSpecification_SatisfiedBy
    {
        [Fact]
        public void GivenTransientEntityThenSatisfySpecification()
        {
            Specification<TestObject1> spec1 = new DirectSpecification<TestObject1>(e => e.Id == 0);

            spec1.IsSatisfiedBy(new TestObject1()).Should().BeTrue();
        }

        [Fact]
        public void GivenNotTransientEntityThenNotSatisfySpecification()
        {
            Specification<TestObject1> spec1 = new DirectSpecification<TestObject1>(e => e.Id == 0);

            spec1.IsSatisfiedBy(new TestObject1(1)).Should().BeFalse();
        }
    }
}

using FluentAssertions;
using Ritter.Infra.Crosscutting.Tests.Mocks;
using System;
using Xunit;

namespace Ritter.Infra.Crosscutting.Tests.Ensuring
{
    public class Ensure_Equal
    {
        [Fact]
        public void ThrowsExceptionGivenNotEqualsPrimitives()
        {
            var a = 1;
            var b = 2;

            Action act = () => Ensure.Equal(a, b);
            act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
        }

        [Fact]
        public void ThrowsExceptionGivenNotEqualsNonPrimitives()
        {
            var a = new TestObject1();
            var b = new TestObject1();

            Action act = () => Ensure.Equal(a, b);
            act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
        }

        [Fact]
        public void ThrowsExceptionGivenLeftNull()
        {
            var b = new TestObject1();

            Action act = () => Ensure.Equal(null, b);
            act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
        }

        [Fact]
        public void ThrowsExceptionGivenRightNull()
        {
            var a = new TestObject1();

            Action act = () => Ensure.Equal(a, null);
            act.Should().Throw<Exception>().And.Message.Should().Be(Messages.BothValuesMustBeEqual);
        }

        [Fact]
        public void EnsureGivenEqualsPrimitives()
        {
            var a = 1;
            var b = 1;

            Action act = () => Ensure.Equal(a, b);
            act.Should().NotThrow<Exception>();
        }

        [Fact]
        public void EnsureGivenEqualsNonPrimitives()
        {
            var a = new TestObject1();
            var b = a;

            Action act = () => Ensure.Equal(a, b);
            act.Should().NotThrow<Exception>();
        }
    }
}

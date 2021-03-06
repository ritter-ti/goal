using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Extensions
{
    public class Enumerable_ForEach
    {
        [Fact]
        public void NotThrowExceptionGivenNotEmptyEnumerable()
        {
            IEnumerable<TestObject1> source = new List<TestObject1>
            {
                new TestObject1 { Id = 1 },
                new TestObject1 { Id = 2 }
            };

            source.ForEach(p =>
            {
                p.Value = p.Id.ToString();
            });

            source.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
            source.ElementAt(0).Value.Should().Be("1");
            source.ElementAt(1).Value.Should().Be("2");
        }

        [Fact]
        public void NotThrowExceptionGivenEmptyEnumerable()
        {
            IEnumerable<TestObject1> source = new List<TestObject1>();
            source.ForEach(p => { });

            source.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNull()
        {
            Action act = () =>
            {
                IEnumerable<TestObject1> source = null;
                source.ForEach(p => { });
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }

        [Fact]
        public void NotThrowExceptionGivenNotEmptyObjectEnumerable()
        {
            TestObject1[] source = new[]
            {
                new TestObject1 { Id = 1 },
                new TestObject1 { Id = 2 }
            };

            source.ForEach(p =>
            {
                TestObject1 obj = p;
                obj.Value = obj.Id.ToString();
            });

            source.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
            source[0].As<TestObject1>().Value.Should().Be("1");
            source[1].As<TestObject1>().Value.Should().Be("2");
        }

        [Fact]
        public void NotThrowExceptionGivenEmptyObjectEnumerable()
        {
            TestObject1[] source = Array.Empty<TestObject1>();
            source.ForEach(p => { });

            source.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void ThrowArgumentNullExceptionGivenNullObjectEnumerable()
        {
            Action act = () =>
            {
                ArrayList source = null;
                source.ForEach(p => { });
            };

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
        }
    }
}

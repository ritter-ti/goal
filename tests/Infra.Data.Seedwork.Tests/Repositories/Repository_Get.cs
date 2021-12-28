using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Infra.Data.Seedwork.Tests.Extensions;
using Goal.Infra.Data.Seedwork.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Seedwork.Tests.Repositories
{
    public class Repository_Get
    {
        [Fact]
        public void ReturnsAnEntityGivenId()
        {
            List<Test> tests = MockTests();

            Mock<DbSet<Test>> mockDbSet = tests
                .AsQueryable()
                .BuildMockDbSet<Test, int>();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var testRepository = new TestRepository(mockDbContext.Object);

            Test test = testRepository.Find(3);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().NotBeNull();
            test.Id.Should().Be(3);
        }

        [Fact]
        public void ReturnsNullGivenId()
        {
            List<Test> tests = MockTests();

            Mock<DbSet<Test>> mockDbSet = tests
                .AsQueryable()
                .BuildMockDbSet<Test, int>();

            var mockDbContext = new Mock<DbContext>();

            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var testRepository = new TestRepository(mockDbContext.Object);
            Test test = testRepository.Find(6);

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().BeNull();
        }

        [Fact]
        public void ReturnsAnEntityGivenIdAsync()
        {
            List<Test> tests = MockTests();

            Mock<DbSet<Test>> mockDbSet = tests
                .AsQueryable()
                .BuildMockDbSet<Test, int>();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var id = Guid.NewGuid();

            var testRepository = new TestRepository(mockDbContext.Object);
            Test test = testRepository.FindAsync(3).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().NotBeNull();
            test.Id.Should().Be(3);
        }

        [Fact]
        public void ReturnsNullGivenIdAsync()
        {
            List<Test> tests = MockTests();

            Mock<DbSet<Test>> mockDbSet = tests
                .AsQueryable()
                .BuildMockDbSet<Test, int>();

            var mockDbContext = new Mock<DbContext>();
            mockDbContext.Setup(p => p.Set<Test>()).Returns(mockDbSet.Object);

            var testRepository = new TestRepository(mockDbContext.Object);
            Test test = testRepository.FindAsync(6).GetAwaiter().GetResult();

            mockDbContext.Verify(x => x.Set<Test>(), Times.Once);
            test.Should().BeNull();
        }

        private static List<Test> MockTests()
        {
            return new List<Test>
            {
                new Test(1),
                new Test(2),
                new Test(3),
                new Test(4),
                new Test(5)
            };
        }
    }
}

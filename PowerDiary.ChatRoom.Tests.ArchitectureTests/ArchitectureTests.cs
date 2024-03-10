using NetArchTest.Rules;
using PowerDiary.ChatRoom.Assembly;
using PowerDiary.ChatRoom.Assembly.Domain;
using Xunit;

namespace PowerDiary.ChatRoom.Tests.ArchitectureTests
{
    public class ArchitectureTests
    {
        [Fact]
        public void DomainLayerShouldNotDependOnTheApplicationLayer()
        {
            var result = Types
              .InAssembly(typeof(DomainAssembly).Assembly)
              .ShouldNot()
              .HaveDependencyOn("PowerDiary.ChatRoom.Application")
              .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ClassesInDomainLayerShouldBeSealed()
        {
            var result = Types
              .InAssembly(typeof(DomainAssembly).Assembly)
              .That()
              .AreClasses()
              .Should()
              .BeSealed()
              .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ConsoleLayerShouldNotReferenceTheDomainLayer()
        {
            var result = Types
              .InAssembly(typeof(ConsoleAssembly).Assembly)
              .ShouldNot()
              .HaveDependencyOn("PowerDiary.ChatRoom.Domain")
              .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ClasesInRepositoryNamespaceShouldBeNamedProperly()
        {
            var result = Types
              .InAssembly(typeof(ConsoleAssembly).Assembly)
              .That()
              .ResideInNamespace("PowerDiary.ChatRoom.Application.Repositories")
              .Should()
              .HaveNameEndingWith("Repository")
              .GetResult();

            Assert.True(result.IsSuccessful);            
        }

        [Fact]
        public void ClasesInServiceNamespaceShouldBeNamedProperly()
        {
            var result = Types
              .InAssembly(typeof(ConsoleAssembly).Assembly)
              .That()
              .ResideInNamespace("PowerDiary.ChatRoom.Application.Services")
              .Should()
              .HaveNameEndingWith("Service")
              .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}

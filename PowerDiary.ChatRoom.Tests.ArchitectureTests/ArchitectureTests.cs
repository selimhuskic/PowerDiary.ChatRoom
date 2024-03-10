using NetArchTest.Rules;
using PowerDiary.ChatRoom.Aplication.Assembly;
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
        public void ApplicationLayerShouldDependOnTheDomainLayer()
        {
            var result = Types
              .InAssembly(typeof(ApplicationAssembly).Assembly)
              .Should()
              .HaveDependencyOn("PowerDiary.ChatRoom.Domain")
              .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}

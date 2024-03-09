using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetArchTest.Rules;
using PowerDiary.ChatRoom.Assembly.Domain;

namespace PowerDiary.ChatRoom.Tests.ArchitectureTests
{
    public class ArchitectureTests
    {
        public void OnionLayeredArchitectureIsApplied()
        {
            var result = Types
              .InAssembly(typeof(DomainAssembly).Assembly)
              .ShouldNot()
              .HaveDependencyOnAny("PowerDiary.ChatRoom.Infrastructure", "PowerDiary.ChatRoom")
              .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}

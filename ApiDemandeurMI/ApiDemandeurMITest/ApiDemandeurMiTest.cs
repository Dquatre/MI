using ApiDemandeurMI.Services;
using IFramework.Infrastructure;

namespace ApiDemandeurMITest
{
    public class Tests
    {
        string passwordTest;

        [SetUp]
        public void Setup()
        {
            passwordTest = PasswordHashService.GenerateHash("Test");
        }

        [Test]
        public void AcceptedCompare()
        {
            Assert.True(PasswordHashService.CompareHash(passwordTest, "Test"));
        }

        [Theory]
        public void RefusedCompare([Values("Voila", "non","")] string password)
        {
            Assert.False(PasswordHashService.CompareHash(passwordTest, password));
        }
    }
}
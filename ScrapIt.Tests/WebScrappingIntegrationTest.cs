using System;
using Xunit;

namespace ScrapIt.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            //Arrange
            var client = new TestClientProvider().Client;

            var response = await client.GetAsync("/api/employee");
            //Act

            //Assert
        }
    }
}

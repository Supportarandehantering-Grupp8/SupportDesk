using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Domain
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_WithEmptyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Customer("", "test@mail.com"));
        }



        [Fact]
        public void Customer_WithInvalidEmail_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Customer("Test", "@mail"));
        }
    }


}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscripitionCommandTest
    {
        [TestMethod]
        public void ShoudReturnErrorWhenNomeIsInvalid()
        {
            var command = new CreateBoletoSubscripitionCommand();
            command.FirstName = "";
            command.Validate();
            Assert.AreEqual(false,command.Valid);
        }
    }
}
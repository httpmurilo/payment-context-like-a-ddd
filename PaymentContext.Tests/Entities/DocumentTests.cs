using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc  = new Document("123",EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }
          [TestMethod]  
        public void ShouldReturnErrorWhenCNPJIsValid()
        {
            var doc  = new Document("93306010000130",EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }
    }
}
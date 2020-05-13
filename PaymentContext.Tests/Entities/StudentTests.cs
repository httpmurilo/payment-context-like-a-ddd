using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AddSignature()
        {
            var subscription = new Subscription(null);
            var student = new Student("Murilo","Eduardo","222","murilo@murilo");
            student.AddSubscription(subscription);
        }
    }
}
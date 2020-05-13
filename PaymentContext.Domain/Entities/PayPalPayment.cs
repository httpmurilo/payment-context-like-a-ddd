using System;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactionCode,
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        string documento,
        string adress,
        string email
        ) : base(paidDate,expireDate,total,totalPaid,documento,payer,adress,email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; set; }
    }
}
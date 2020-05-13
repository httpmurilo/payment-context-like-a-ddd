using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        private IList<Subscription> _subscriptions;
        public Student(string firstNome, string lastName, string document, string email)
        {
            FirstNome = firstNome;
            LastName = lastName;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
        }

        public string FirstNome { get; private set; }
        public string LastName { get; private  set; }
        public string Document { get; private  set; }
        public string Email { get; private  set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }
        public string Address { get; set; }

        public void AddSubscription(Subscription subscription)
        {
            //se ja tiver uma assinatura ativa, cancela
            //cancela todas as outras assinatura e coloca essa como principal
            foreach(var sub in Subscriptions)
            {
                sub.Inactivate();
            }
            _subscriptions.Add(subscription);
        }
    }
}
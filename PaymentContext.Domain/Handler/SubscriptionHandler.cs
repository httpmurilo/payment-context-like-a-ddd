using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repository;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handler
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscripitionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailservice)
        {
            _repository = repository;
            _emailService = emailservice;
        }
        public ICommandResult Handle(CreateBoletoSubscripitionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Nao foi possivel finalizar seu cadastro");
            }
            //Verificar se Documento ja está cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document","Este CPF ja existe");
            }
            //Verificar se email já esta cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Email","Este Email ja existe");
            }
            //Gerar os VOs
            var name  = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.City,command.State, command.Country, command.ZipCode);
            
            //Gerar as entidades
            var student = new Student(name, document, email); 
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment  = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate,command.ExpireDate,command.Total,command.TotalPaid,command.Payer,new Document(command.PayerDocument,command.PayerDocumentType),address,email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar  as validações
            AddNotifications(name,document,email,student);

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.Name.ToString(),email.Adress,"Bem vindo", "sua assinatura esta tudo ok");

            //retornar as informacoes
            return new CommandResult(true,"Assinatura com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Verificar se Documento ja está cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document","Este CPF ja existe");
            }
            //Verificar se email já esta cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Email","Este Email ja existe");
            }
            //Gerar os VOs
            var name  = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.City,command.State, command.Country, command.ZipCode);
            
            //Gerar as entidades
            var student = new Student(name, document, email); 
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment  = new PayPalPayment(command.TransactionCode, command.PaidDate,command.ExpireDate,command.Total,command.TotalPaid,command.Payer,new Document(command.PayerDocument,command.PayerDocumentType),address,email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar  as validações
            AddNotifications(name,document,email,student);

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.Name.ToString(),email.Adress,"Bem vindo", "sua assinatura esta tudo ok");

            //retornar as informacoes
            return new CommandResult(true,"Assinatura com sucesso");
       
        }
    }
}
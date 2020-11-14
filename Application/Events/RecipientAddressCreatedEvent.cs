using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.Data.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Events
{
    public class RecipientAddressCreatedEvent: DomainEvent
    {
        public RecipientAddressCreatedEvent(RecipientAddress recipientAddress)
        {
            RecipientAddress = recipientAddress;
        }

        public RecipientAddress RecipientAddress { get; }
    }

    public class RecipientAddressCreatedEventHandler : INotificationHandler<DomainEventNotification<RecipientAddressCreatedEvent>>
    {
        private readonly ILogger<RecipientAddressCreatedEventHandler> _logger;

        public RecipientAddressCreatedEventHandler(ILogger<RecipientAddressCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<RecipientAddressCreatedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("App Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
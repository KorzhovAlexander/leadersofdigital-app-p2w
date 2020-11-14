using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.Data.Entities;
using Application.Data.Enums;
using Application.Data.Persistence;
using Application.Events;
using Application.Feature.RecipientAddress.Commands.CreateByFile;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;

namespace Application.Feature.RecipientAddress.Commands.CreateRecipientAddress
{
    public class CreateRecipientAddressCommand : IRequest<Unit>
    {
        public IEnumerable<ApiResponse> Addresses { get; set; }
        public string CurrentUserId { get; set; }
    }

    public class CreateRecipientAddressCommandHandler : IRequestHandler<CreateRecipientAddressCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IDateTime _dateTime;

        public CreateRecipientAddressCommandHandler(ApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(CreateRecipientAddressCommand request, CancellationToken cancellationToken)
        {
            var usersAddresses = request.Addresses.Select(address => new Data.Entities.UsersAddresses
                {
                    Accuracy = address.Addr.Accuracy,
                    Index = address.Addr.Index,
                    State = address.State,
                    InputAddress = address.Addr.Inaddr,
                    OutputAddress = address.Addr.Outaddr,
                    UserId = request.CurrentUserId,
                    ElementAddresses = FormElementAddresses(address.Addr.Element),
                    Delivery = address.Addr.Delivery,
                    Direct = (DirectEnum) address.Addr.Direct,
                    Origin = address.Addr.Origin,
                    Unparsed = address.Addr.Unparsed,
                    AddrType = (AddressType) address.Addr.AddrType,
                    DeliveryArea = address.Addr.DeliveryArea,
                    Created = _dateTime.Now
                })
                .ToList();


            var recipientAddress = new List<Data.Entities.RecipientAddress>();
            foreach (var requestAddress in request.Addresses)
            {
                recipientAddress.Add(new Data.Entities.RecipientAddress
                {
                    Address = requestAddress.Addr.Inaddr,
                });
            }


            await _context.RecipientAddresses.AddRangeAsync(recipientAddress, cancellationToken);
            await _context.AddRangeAsync(usersAddresses, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var address in recipientAddress)
            {
                address.DomainEvents.Add(new RecipientAddressCreatedEvent(address));
            }
            
            return Unit.Value;
        }

        private static IList<ElementAddresses> FormElementAddresses(IEnumerable<Element> elements)
        {
            return elements.Select(element => new ElementAddresses
            {
                Value = element.Val,
                StName = element.StName,
                TName = element.TName,
                Type = GetContent(element.Content),
                Historical = element.Historical,
                Origin = element.Origin,
            }).ToList();
        }

        private static TypeElementAddresses GetContent(string content)
        {
            return content switch
            {
                "C" => TypeElementAddresses.Страна,
                "R" => TypeElementAddresses.Регион,
                "A" => TypeElementAddresses.Район,
                "P" => TypeElementAddresses.НаселенныйПункт,
                "T" => TypeElementAddresses.ВнутригородскаяТерритория,
                "S" => TypeElementAddresses.Улица,
                "N" => TypeElementAddresses.НомерДома,
                "L" => TypeElementAddresses.Литера,
                "D" => TypeElementAddresses.Дробь,
                "E" => TypeElementAddresses.Корпус,
                "B" => TypeElementAddresses.Строение,
                "F" => TypeElementAddresses.Помещение,
                "BOX" => TypeElementAddresses.АбонентскийЯщик,
                "OPS" => TypeElementAddresses.ОтделениеПочтовойСвязи,
                "M" => TypeElementAddresses.ВойсковаяЧасть,
                _ => 0
            };
        }
    }
}
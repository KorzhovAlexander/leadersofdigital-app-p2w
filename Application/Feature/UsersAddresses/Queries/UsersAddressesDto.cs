using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using Application.Data.Entities;
using Application.Data.Enums;

namespace Application.Feature.UsersAddresses.Queries
{
    public class UsersAddressesDto : IMapFrom<Data.Entities.UsersAddresses>
    {
        public int Id { get; set; }
        public string Accuracy { get; set; }
        public string InputAddress { get; set; }
        public string OutputAddress { get; set; }
        public int Index { get; set; }
        public string State { get; set; }
        public string UserId { get; set; }
        public AddressType AddrType { get; set; }
        public int Delivery { get; set; }
        public int DeliveryArea { get; set; }
        public DirectEnum Direct { get; set; }
        public int Origin { get; set; }
        public string Unparsed { get; set; }
        public DateTime Created { get; set; }

        // public IList<ElementAddresses> ElementAddresses { get; set; }
    }
}
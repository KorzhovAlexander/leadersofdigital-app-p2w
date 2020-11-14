using Application.Common.Mappings;
using Application.Data.Common;

namespace Application.Feature.RecipientAddress.Queries.GetRecipientAddress
{
    public class RecipientAddressDto : AuditableEntity, IMapFrom<Data.Entities.RecipientAddress>
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }
}
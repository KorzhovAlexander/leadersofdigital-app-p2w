using Application.Data.Enums;

namespace Application.Data.Entities
{
    /// <summary>
    /// Элементы адреса (регион, улица, и тд.)
    /// </summary>
    public class ElementAddresses
    {
        public int Id { get; set; }
        public string StName { get; set; }
        public string TName { get; set; }
        public string Value { get; set; }

        public int Historical { get; set; }
        
        public int Origin { get; set; }
        public TypeElementAddresses Type { get; set; }
        
        public int UsersAddressesId { get; set; }
        public UsersAddresses UsersAddresses { get; set; }
    }
}
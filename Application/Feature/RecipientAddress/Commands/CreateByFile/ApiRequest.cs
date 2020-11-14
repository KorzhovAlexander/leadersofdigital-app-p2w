using System.Collections.Generic;

namespace Application.Feature.RecipientAddress.Commands.CreateByFile
{
    public class ApiRequest
    {
        public string version { get; set; } = "demo";
        public string reqId { get; set; } = "53fb9daa-7f06-481f-aad6-c6a7a58ec0bb";
        public IList<AddrDto> addr { get; set; }
    }

    public class AddrDto
    {
        public string val { get; set; }
    }
}
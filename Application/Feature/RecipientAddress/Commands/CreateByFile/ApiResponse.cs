using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Feature.RecipientAddress.Commands.CreateByFile
{
    public class ApiResponse
    {
        [JsonPropertyName("version")] public string Version { get; set; }
        [JsonPropertyName("state")] public string State { get; set; }
        [JsonPropertyName("index")] public Index Index { get; set; }

        [JsonPropertyName("addr")] public Addr Addr { get; set; }
    }

    public class Addr
    {
        [JsonPropertyName("inaddr")] public string Inaddr { get; set; }
        [JsonPropertyName("outaddr")] public string Outaddr { get; set; }
        [JsonPropertyName("addrType")] public int AddrType { get; set; }
        [JsonPropertyName("direct")] public int Direct { get; set; }
        [JsonPropertyName("delivery")] public int Delivery { get; set; }
        [JsonPropertyName("deliveryArea")] public int DeliveryArea { get; set; }
        [JsonPropertyName("accuracy")] public string Accuracy { get; set; }
        [JsonPropertyName("index")] public int Index { get; set; }
        [JsonPropertyName("origin")] public int Origin { get; set; }
        [JsonPropertyName("unparsed")] public string Unparsed { get; set; }
        [JsonPropertyName("element")] public IList<Element> Element { get; set; }
    }

    public class Element
    {
        [JsonPropertyName("guid")]public string Guid { get; set; }
        [JsonPropertyName("val")]public string Val { get; set; }
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("origin")]public int Origin { get; set; }
        [JsonPropertyName("historical")]public int Historical { get; set; }
        [JsonPropertyName("stname")]public string StName { get; set; }
        [JsonPropertyName("tname")]public string TName { get; set; }
    }

    public class Index
    {
        [JsonPropertyName("inindex")] public string Inindex { get; set; }
        [JsonPropertyName("disablecode")] public string Disablecode { get; set; }
    }
}
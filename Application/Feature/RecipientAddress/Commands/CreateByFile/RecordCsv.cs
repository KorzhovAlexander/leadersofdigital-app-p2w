using CsvHelper.Configuration.Attributes;

namespace Application.Feature.RecipientAddress.Commands.CreateByFile
{
    public class RecordCsv
    {
        [Index(0)]public string Address { get; set; }
        
        // [Name("Identifier")] public int Id { get; set; }
        //
        // [Index(1)] public string Name { get; set; }
        //
        // [BooleanTrueValues("yes")]
        // [BooleanFalseValues("no")]
        // public bool IsBool { get; set; }
        //
        // [Constant("bar")] public string Constant { get; set; }
        //
        // [Optional] public string Optional { get; set; }
        //
        // [Ignore] public string Ignored { get; set; }
    }
}
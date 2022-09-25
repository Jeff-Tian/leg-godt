namespace Web.Models;

public class Corporation
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CorpId { get; set; }
    public string CorpSecret { get; set; }

    public static Corporation GetFakeCorp()
    {
        return new Corporation
        {
            Name = "hardway",
            CorpId = "123",
            CorpSecret = "abc"
        };
    }
}
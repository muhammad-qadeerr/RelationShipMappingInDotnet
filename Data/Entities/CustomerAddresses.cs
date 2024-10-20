namespace DOTNETRELATIONS.Data.Entities;

public class CustomerAddresses
{
    public int Id { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public int CustomerId { get; set; }

    // Navigation Property for 1:1 relationship i.e 1 CustomerAddresses will belong to 1 Customer.
    public Customer? Customer { get; set; }


}


using System.ComponentModel.DataAnnotations;

namespace DOTNETRELATIONS.Data.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }

    // Navigation Property to define 1:M relationship i.e 1 Customer Many CustomerAddresses
    public List<CustomerAddresses> CustomerAddresses { get; set; }
}


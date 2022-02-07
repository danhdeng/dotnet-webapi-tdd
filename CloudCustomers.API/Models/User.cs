namespace CloudCustomers.API.Models;

public class User
{
    public Address Address { get; set; }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}

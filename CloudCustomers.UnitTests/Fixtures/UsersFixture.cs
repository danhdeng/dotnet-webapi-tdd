

using CloudCustomers.API.Models;
using System.Collections.Generic;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture {
    public static List<User> GetAllTestUsers() => new()
    {
        new User
        {
            Id = 1,
            Name = "test one",
            Email = "test1@mail.com",
            Address = new Address()
            {
                Street = "1 road test",
                City = "Toronto",
                ZipCode = "012355"
            }
        },
        new User
        {
            Id = 2,
            Name = "test two",
            Email = "test2@mail.com",
            Address = new Address()
            {
                Street = "2 road test",
                City = "Toronto",
                ZipCode = "012355"
            }
        },
        new User
        {
            Id = 3,
            Name = "test three",
            Email = "test3@mail.com",
            Address = new Address()
            {
                Street = "3 road test",
                City = "Toronto",
                ZipCode = "012355"
            }
        }
    };
}

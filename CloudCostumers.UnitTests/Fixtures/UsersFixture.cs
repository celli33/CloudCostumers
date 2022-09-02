using CloudCostumers.API.Models;

namespace CloudCostumers.UnitTests.Fixtures;
public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new()
        {
            Id = 1,
            Name = "Jane",
            Address = new Address { Street = "calle", City = "Ciudad", ZIpCode = "72030" }
        },
        new()
        {
            Id = 1,
            Name = "Jane",
            Address = new Address { Street = "calle", City = "Ciudad", ZIpCode = "72030" }
        },
        new()
        {
            Id = 1,
            Name = "Jane",
            Address = new Address { Street = "calle", City = "Ciudad", ZIpCode = "72030" }
        }
    };

}


using RIA.Client.Models;

namespace RIA.Client;

public static class TestCustomerGenerator
{
    private static readonly Random _random = new();
    private static readonly string[] FirstNames = [
        "Leia", "Sadie", "Jose", "Sara", "Frank",
        "Dewey", "Tomas", "Joel", "Lukas", "Carlos" ];
    private static readonly string[] LastNames = [
        "Harrison", "Ronan", "Drew", "Powell", "Larsen",
        "Chan", "Anderson", "Lane", "Ray", "Liberty"];
    private static readonly int MinAge = 10;
    private static readonly int MaxAge = 90;

    public static IEnumerable<IEnumerable<Customer>> GenerateCustomers(int count, int startingId = 1)
    {
        var customerGroups = new List<List<Customer>>();
        var customers = GenerateRandomCustomers(count, startingId);
        var i = 0;
        while (i < customers.Count) {
            int groupSize = _random.Next(2, 5);
            var tempGroup = new List<Customer>();
            var lastIndex = Math.Min(i + groupSize, customers.Count) - 1;
            for (int j = i; j <= lastIndex; j++)
            {
                tempGroup.Add(customers[j]);
            }
            customerGroups.Add(tempGroup);
            i += groupSize;
        }
        return customerGroups;
    }

    private static IList<Customer> GenerateRandomCustomers(int count, int startingId = 1)
    {
        var customers = new List<Customer>();
        var endingId = count + startingId;
        for (int id = startingId; id < endingId; id++)
        {
            customers.Add(new Customer
            {
                Id = id,
                LastName = LastNames[_random.Next(0, LastNames.Length)],
                FirstName = FirstNames[_random.Next(0, FirstNames.Length)],
                Age = _random.Next(MinAge, MaxAge + 1)
            });
        }
        return customers;
    }
}
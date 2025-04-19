// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using Newtonsoft.Json;
using RIA.Client;

Console.WriteLine("Test Client to Exercise RIA.API Functionality");

var riaApiBaseAddress = "https://riaapi-img-latest.onrender.com/";
var createCustomersEndpoint = "api/customers";
var getCustomersEndpoint = "api/customers";

var groups = TestCustomerGenerator.GenerateCustomers(5, 1);
foreach (var cg in groups)
{
    System.Console.WriteLine(JsonConvert.SerializeObject(cg));
    System.Console.WriteLine();
}

System.Console.WriteLine();
System.Console.WriteLine("Calling the API...");
System.Console.WriteLine("[Note]: Since the host is a free service, first call is going to " +
"take about a min to complete as the container would be redeployed after 15 min of inactivity.");
System.Console.WriteLine();


await Parallel.ForEachAsync(groups, async (group, ct) =>
{
    HttpClient httpClient = new()
    {
        Timeout = new TimeSpan(0, 1, 0),
        BaseAddress = new Uri(riaApiBaseAddress)
    };

    using (httpClient)
    {
        var resp = await httpClient.PostAsJsonAsync(createCustomersEndpoint, group);
        string output = "";
        if (resp.IsSuccessStatusCode)
        {
            var res = await resp.Content.ReadAsStringAsync();
            output = res;
        }
        else
        {
            var res = await resp.Content.ReadAsStringAsync();
            output = $"{resp.StatusCode} : {res}";
        }

        var getRes = await httpClient.GetStringAsync(getCustomersEndpoint);
        output += "\r\n";
        output += getRes;

        System.Console.WriteLine(output);
    }
});